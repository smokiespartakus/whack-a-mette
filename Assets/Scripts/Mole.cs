using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
	public bool WasHit { get {return wasHit;}}
	float yStart = -0.14f;
	float yStop = 0.05f; 
	public float moveTimeUp = 0.3f;
	public float moveTimeDown = 0.1f;
	MoleSprite moleSprite;
	[SerializeField]
	float level = 1;

	bool ended = false;
	int points = 1;
	bool wasHit;
	public delegate void OnMoleHit(int points, Mole mole);
	public OnMoleHit onHit;

	void OnEnable() {
	}
	void OnMouseDown() {
		Debug.Log("Mouse DOWN MOLE" + this.name);
		//OnHit?.Invoke();
		if (CanHit()) Hit();
	}

	public void SetSprite(MoleSprite sprite) {
		moleSprite = sprite;
		sprite.transform.SetParent(transform);
	}

	public void SetRandomLogo() {
		if (moleSprite != null) moleSprite.SetRandomLogo();
	}

	public void RunHit() {
		wasHit = true;
	}
	public bool IsEnded() {
		return ended;
	}

	public void SetLevel(float newLevel) {
		level = newLevel;
	}
	public bool IsActive() {
		return gameObject.activeSelf;
	}

	public void Show() {
		if (moleSprite != null) moleSprite.SetRandomLogo();
		gameObject.SetActive(true);
		LTDescr tween = LeanTween.moveLocalY(gameObject, yStop, moveTimeUp);
		tween.setOnComplete(() => {
			if (IsActive())
				StartCoroutine(HideInSeconds(RandomHelper.GetFloatXToY(0.2f, 2.0f)));
		});
	}

	public void Hide() {
		LeanTween.cancel(gameObject);
		LTDescr tween = LeanTween.moveLocalY(gameObject, yStart, moveTimeDown);
		tween.setOnComplete(() => {
			gameObject.SetActive(false);
			End();
		});
	}

	IEnumerator HideInSeconds(float time) {
		yield return new WaitForSeconds(time);
		Hide();
	}

	void End() {
		ended = true;
	}
	public void Reset() {
		LeanTween.cancel(gameObject);
		transform.localPosition = new Vector3(transform.localPosition.x, yStart, transform.localPosition.z);
		ended = false;
		wasHit = false;
		gameObject.SetActive(false);
	}
	void Hit() {
		onHit?.Invoke(points, this);
		Hide();
	}
	bool IsOnTop() {
		return transform.localPosition.y >= yStop;
	}
	bool IsOnBottom() {
		return transform.localPosition.y <= yStart;
	}
	bool CanHit() {
		return !wasHit && (!moleSprite || !moleSprite.logo || moleSprite.logo.canBeHit);
	}


}
