// Adopted from a tutorial by sebastian lague 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	// Set prefabs in editor
	public Transform tilePrefab; 
	public Transform obstaclePrefab; 
	public Vector2 mapSize;
	// This is the outline of tiles
	[Range(0,1)]
	public float outlinePercent;
	// Obstacle Density 
	[Range(0,1)]
	public float obstacleDensity; 
	// Storing Coordinates for tiles during Obstacle Generation
	public float tileSize;
	List<Coord> allTileCoords; 
	Queue<Coord> shuffledTileCoords; 
	List<Coord> shuffledOpenTileCoords;
	List<Coord> cauldronCoords;
	Transform[,] tileMap;

	// The seed used for obstacle generation - can be set on start of each runtime/level 
	public int seed = 001;
	Coord mapCenter;

	void Start(){
		GenerateMap(); 	
	}

	// Primary Map Generation Method
	public void GenerateMap(){
		tileMap = new Transform[(int)mapSize.x, (int)mapSize.y];

		// Generate coordinates for all tiles
		allTileCoords = new List<Coord> ();
		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y ++) {
				allTileCoords.Add(new Coord(x,y));
			}
		}
		shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray (), seed));
		// Calculate the center of the map
		mapCenter = new Coord((int)mapSize.x/2, (int)mapSize.y/2);

		string holderName = "Generated Map";
		if (transform.Find (holderName)) {
			DestroyImmediate(transform.Find(holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y ++) {
				Vector3 tilePosition = CoordToPosition(x, y);
				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(0,0,0)) as Transform;
				newTile.localScale = Vector3.one * (1-outlinePercent) * tileSize;
				newTile.parent = mapHolder;
				tileMap[x,y] = newTile;
			}
		}
		
		// Spawning Cauldrons
		Vector3 Cauldron1 = new Vector3(1.5f - mapSize.x/2, mapSize.y - 1.5f - mapSize.y/2, -3);
		//Vector3 Cauldron2 = new Vector3(mapSize.x - 1.5f - mapSize.x/2, mapSize.y - 1.5f - mapSize.y/2, -3);
		Vector3 Cauldron3 = new Vector3(mapSize.x - 1.5f - mapSize.x/2, 1.5f - mapSize.y/2, -3);
		//Vector3 Cauldron4 = new Vector3(1.5f - mapSize.x/2, 1.5f - mapSize.y/2, -3);
		List<Vector3> cauldronVector = new List<Vector3>() {Cauldron1, Cauldron3};
		for (int i = 0;  i < cauldronVector.Count; i++ ){
			GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Cauldron"), cauldronVector[i], Quaternion.identity);
			tempObj.transform.parent = mapHolder;
			tempObj.name = "Cauldron" + (i+1);
            GameObject tempPlayer = GameObject.Find("player" + (i+1));
            Debug.Log(tempPlayer);
            tempPlayer.GetComponent<PlayerController>().playerCauldron = tempObj.GetComponent<CauldronController>();
            tempObj.GetComponent<CauldronController>().player = tempPlayer.GetComponent<PlayerController>();
		}

		// Spawning Obstacles 
		bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

		int obstacleCount = (int)(mapSize.x * mapSize.y * obstacleDensity);
		int currentObstacleCount = 0;
		List<Coord> allOpenCoords = new List<Coord>(allTileCoords);

		for (int i=0; i < obstacleCount; i ++){
			Coord randomCoord = GetRandomCoord();
			obstacleMap[randomCoord.x, randomCoord.y] = true;
			currentObstacleCount ++;

			if (randomCoord != mapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount)) { 
				Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);	
				Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity) as Transform; 
				newObstacle.parent = mapHolder;
				newObstacle.localScale = Vector3.one * (1-outlinePercent) * tileSize;

				allOpenCoords.Remove(randomCoord);
			}
			else{
				obstacleMap[randomCoord.x, randomCoord.y] = false;
				currentObstacleCount --; 
			}
		}

		shuffledOpenTileCoords = new List<Coord>(Utility.ShuffleArray(allOpenCoords.ToArray(), seed));

	}

	// Ensure that no tile is unreachable at the start of the game
	bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount){
		bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
		Queue<Coord> queue  = new Queue<Coord> ();
		queue.Enqueue (mapCenter);
		mapFlags[mapCenter.x, mapCenter.y] = true;

		int accessibleTileCount = 1;

		while (queue.Count > 0){
			Coord tile = queue.Dequeue();

			for (int x = -1; x <= 1; x++){
				for (int y = -1; y <= 1; y++){
					int neighbourX = tile.x + x;
					int neighbourY = tile.y + y;
					if (x == 0 ||  y == 0){
						if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) 
						&& neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1)){
							if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY]) {
								mapFlags[neighbourX,neighbourY] = true;
								queue.Enqueue(new Coord(neighbourX, neighbourY));
								accessibleTileCount ++;
							}
						}
					}
				}
			}
		}

		int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstacleCount);
		return targetAccessibleTileCount == accessibleTileCount;

	}

	Vector3 CoordToPosition(int x, int y) {
		return new Vector3(-mapSize.x/2 + 0.5f + x, -mapSize.y/2 + 0.5f + y, 0) * tileSize;
	}

	// We use queues to the nature of a fisher yates shuffle, for obstacles
	public Coord GetRandomCoord() {
		Coord randomCoord = shuffledTileCoords.Dequeue ();
		shuffledTileCoords.Enqueue (randomCoord);
		return randomCoord;
	}

	public Transform GetRandomOpenTile(){
		Coord randomCoord = shuffledOpenTileCoords[Random.Range(0, shuffledOpenTileCoords.Count)];
		return tileMap[randomCoord.x, randomCoord.y];
	}

	// Overriding the == and != operators for map coordinates
	public struct Coord {
		public int x;
		public int y; 

		public Coord(int _x, int _y){
			x = _x;
			y = _y; 
		}

		public static bool operator == (Coord c1, Coord c2){
			return c1.x == c2.x && c1.y == c2.y;
		}

		public static bool operator != (Coord c1, Coord c2){
			return !(c1 == c2); 
		}
	}
	
	// Methods to grab X/Y lengths
	public float getX() {
		return mapSize.x;
	}
	
	public float getY() {
		return mapSize.y;
	}
}
