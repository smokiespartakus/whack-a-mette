using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
	public GameObject uiGameobject;
	public GameObject moleGameObject;

	public Texture2D cursorTexture;
	
	Vector2 resolution;
	void OnEnable() {
		uiGameobject.SetActive(true);
		if (moleGameObject != null) {
			moleGameObject.SetActive(false);
		}
	}

	void Start() {
		Cursor.SetCursor(cursorTexture, new Vector2(0, cursorTexture.height / 2), CursorMode.Auto);
		resolution = new Vector2(Screen.width, Screen.height);
		SetFieldOfView();
	}

	void Update() {
		if (resolution != null && (resolution.x != Screen.width || resolution.y != Screen.height)) {
			resolution.x = Screen.width;
			resolution.y = Screen.height;
			SetFieldOfView();
		}
	}

	void SetFieldOfView() {
		float ratio = resolution.x / Mathf.Max(1, resolution.y);
		float fov = 60;
		float ninebysixteen = 9f/16f;
		//Debug.Log("RES CHANGED " + resolution + " ratio=" + ratio + " 9:16="+ninebysixteen);
		if (ratio < ninebysixteen) {
			// 1:2 = 66; 9/16 - 1/2 == 0.0625 
			// 1440:3120 = 71; 9/16 - 1440/3120 ~= 0.1
			fov += 110 * (ninebysixteen - ratio); 
		}
			//GetComponent<Camera>().fieldOfView = fov;
		Camera.main.fieldOfView = fov;
	}
}
