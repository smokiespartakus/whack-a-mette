using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
	float speed = 1;
	float xStart = -0.2f;
	float yStart = 0f;
	float yStop = 0.8f;

	bool movingUp = false;
	bool movingDown = false;

	bool onTop = false;

	bool ended = false;
	Hole hole = null;
	void Enable() {
		// set speed and how long to keep it up?
	}
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void AddToHole(Hole h) {
		transform.SetParent(h.transform);
		Reset();
		hole = h;
		movingUp = true;
	}

	public void RunUpdate(float deltaTime) {
		if (IsMoving()) {
			transform.Translate(Vector3.up * deltaTime * (movingUp ? 1 : -1));
			if (movingUp && IsOnTop()) {
				movingUp = false;
				transform.localPosition = new Vector3(transform.localPosition.x, yStop, transform.localPosition.z);
			} else if (movingDown && IsOnBottom()) {
				End();	
			}
		} else if (IsOnTop()) {
			if(RandomHelper.PercentCheck(1)) {
				movingDown = true;
			}
		}
	}
	public bool IsEnded() {
		return ended;
	}
	void End() {
		ended = true;
		hole.RemoveMole();
	}
	void Reset() {
		movingDown = false;
		movingUp = false;
		transform.localPosition = new Vector3(xStart,yStart,0);
		ended = false;
		hole = null;
	}
	bool IsOnTop() {
		return transform.localPosition.y >= yStop;
	}
	bool IsOnBottom() {
		return transform.localPosition.y <= yStart;
	}
	bool IsMoving() {
		return movingUp || movingDown;
	}

}
