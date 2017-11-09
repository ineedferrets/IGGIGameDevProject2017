using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronHealthSlider : MonoBehaviour {

    public Transform cauldron;
    public Vector3 localOffset;
    public Vector3 screenOffset;

    RectTransform _myCanvas;

	// Use this for initialization
	void Start () {
        _myCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 worldPoint = cauldron.TransformPoint(localOffset);
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

        viewportPoint -= 0.5f * Vector3.one;
        viewportPoint.z = 0;

        Rect rect = _myCanvas.rect;
        viewportPoint.x *= rect.width;
        viewportPoint.y *= rect.height;

        transform.localPosition = viewportPoint + screenOffset;
	}
}
