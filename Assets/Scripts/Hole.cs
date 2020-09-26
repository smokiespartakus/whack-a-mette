using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
	Mole mole = null;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	public void AddMole(Mole m) {
		mole = m;
		m.AddToHole(this);
		Debug.Log("Add Mole!");
	}

	public void RemoveMole() {
		mole = null;
	}

	public bool IsEmpty() {
		return mole == null;
	}
}
