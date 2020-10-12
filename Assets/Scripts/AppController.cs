using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
	public GameObject UIGameobject;
	void OnEnable() {
		UIGameobject.SetActive(true);
	}
}
