using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

	public Wave[] waves;
	public List<Ingredient> ingredients;

	public int itemsRemainingTospawn;
	public float nextSpawnTime;

	private MapGenerator map;

	Wave currentWave; 
	int currentWaveNumber;

	void Awake(){
		SetUpIngredients();
		map = FindObjectOfType<MapGenerator>();
		NextWave();
	}

	void Update(){

		if(itemsRemainingTospawn > 0 && Time.time > nextSpawnTime){
			itemsRemainingTospawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

			StartCoroutine(SpawnItem());
		}
	}

	IEnumerator SpawnItem(){
		float spawnDelay = 1f;
		

		Transform randomTile = map.GetRandomOpenTile();
		float spawnTimer = 0f; 

		while (spawnTimer < spawnDelay){
			
			spawnTimer += Time.deltaTime;
			yield return null;
		}
		ingredients[Random.Range(0, ingredients.Count)].SpawnObject(randomTile.position);
	}

	void NextWave(){
		currentWaveNumber ++;
		currentWave = waves[currentWaveNumber-1];

		itemsRemainingTospawn = currentWave.itemCount;

		nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
		Debug.Log(nextSpawnTime);
	}

	void SetUpIngredients() {
		ingredients = new List<Ingredient>();
		ingredients.Add(new Ingredient(Ingredient.Type.blue));
		ingredients.Add(new Ingredient(Ingredient.Type.red));
	}

	[System.Serializable]

	public class Wave {
		public int itemCount;
		public float timeBetweenSpawns;
	}

}
