using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
	public GameObject uiGameobject;
	public GameObject moleGameObject;

	public Texture2D cursorTexture;
	void OnEnable() {
		uiGameobject.SetActive(true);
		if (moleGameObject != null) {
			moleGameObject.SetActive(false);
		}
	}

	void Start() {
		Cursor.SetCursor(cursorTexture, new Vector2(0, cursorTexture.height / 2), CursorMode.Auto);
	}
}
