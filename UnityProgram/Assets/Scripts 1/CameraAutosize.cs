using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[ExecuteInEditMode]
public class CameraAutosize: MonoBehaviour {
	public GameObject parentCanvasGO;
	public GameObject leftCanvasGO;
	public GameObject rightCanvasGO;

	public GameObject[] camerasGameObjects;

	private Canvas parentCanvas;
	private RectTransform leftCanvas;
	private RectTransform rightCanvas;
	private List<Camera> cameras;

	private Vector2 screenResolution;

	private void OnEnable() {
		parentCanvas = parentCanvasGO.GetComponent<Canvas>();
		leftCanvas = leftCanvasGO.GetComponent<RectTransform>();
		rightCanvas = rightCanvasGO.GetComponent<RectTransform>();

		cameras = new List<Camera>();

		for(int i = 0; i < camerasGameObjects.Length; i++) {
			cameras.Add(camerasGameObjects[i].GetComponent<Camera>());
		}

		screenResolution = new Vector2(Screen.width, Screen.height);
		UpdateCameraSize();
		
	}

	private void Update() {
		if (screenResolution.x != Screen.width || screenResolution.y != Screen.height) {
			UpdateCameraSize();
		}
	}

	public void UpdateCameraSize() {
		float rectTransformsBusySpace = (leftCanvas.rect.width + rightCanvas.rect.width) * parentCanvas.scaleFactor;

		float cameraNewSizeX = 1 - rectTransformsBusySpace / Screen.width;
		float cameraNewPositionX = (1 - cameraNewSizeX) * 0.5f;

		float cameraNewSizeY = 1;

		if (Screen.height > Screen.width * cameraNewSizeX) {
			cameraNewSizeY = Screen.width * cameraNewSizeX / Screen.height;
		}

		float cameraNewPositionY = (1 - cameraNewSizeY) * 0.5f;

		for (int i = 0; i < cameras.Count; i++) {
			cameras[i].rect = new Rect(new Vector2(cameraNewPositionX, cameraNewPositionY), new Vector2(cameraNewSizeX, cameraNewSizeY));
		}

		screenResolution = new Vector2(Screen.width, Screen.height);
	}
}
