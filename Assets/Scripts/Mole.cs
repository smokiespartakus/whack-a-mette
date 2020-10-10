using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
	public List<Logo> logos;
	public bool WasHit { get {return wasHit;}}
	float initSpeed = 1;
	[SerializeField]
	float speed = 1;
	float xStart = -0.2f;
	float yStart = 0f;
	float yStop = 0.8f;

	bool movingUp = false;
	bool movingDown = false;

	bool onTop = false;

	bool ended = false;
	Hole hole = null;
	Logo logo;
	int points = 1;
	bool wasHit;

	public delegate void OnMoleHit(int points, Mole mole);
	public OnMoleHit onHit;

	void OnEnable() {
		// set speed and how long to keep it up?
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
		SetRandomLogo();
		speed = initSpeed;
	}
	void OnMouseDown() {
		Debug.Log("Mouse DOWN" + logo);
		if (canHit()) Hit();
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
			transform.Translate(Vector3.up * deltaTime * (movingUp ? speed : -1 * speed));
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
	public void SetRandomLogo() {
		logo = RandomHelper.GetListElement<Logo>(logos);
		if (logo != null) {
			logo.gameObject.SetActive(true);
			Debug.Log("RANDOM " + logo.name);
		} else {
			throw new System.Exception("No logos found");
		}
	}

	public void RunHit() {
		movingDown = true;
		movingUp = false;
		speed *= 2;
		wasHit = true;
	}
	public bool IsEnded() {
		return ended;
	}

	public void SetSpeed(float newSpeed) {
		initSpeed = newSpeed;
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
		wasHit = false;
	}
	void Hit() {
		onHit?.Invoke(points, this);
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

	bool canHit() {
		return !wasHit;
	}

}
