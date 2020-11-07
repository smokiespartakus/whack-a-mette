using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSprite : MonoBehaviour
{
	public List<Logo> logos;
	//public bool WasHit { get {return wasHit;}}
	public Logo logo;
	
	public void SetRandomLogo() {	
		Reset();
		gameObject.SetActive(true);
		logo = RandomHelper.GetListElement<Logo>(logos);
		if (logo != null) {
			logo.gameObject.SetActive(true);
			// Debug.Log("RANDOM " + logo.name);
		} else {
			throw new System.Exception("No logos found");
		}
	}

	public void Reset() {
		foreach (Transform t in transform) {
			transform.gameObject.SetActive(false);
		}
		foreach (Logo logo in logos) {
			logo.gameObject.SetActive(false);
		}
	}

}
