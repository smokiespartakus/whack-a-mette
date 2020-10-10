using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

	public List<Hole> holes;
	public Mole mole;
	[SerializeField]
	private float speed = 1;
	[SerializeField]
	
	private float timePassed = 0;
	private bool clearMoles;
	List<Mole> moles = new List<Mole>();
	
	public bool IsPlaying {
		get {
			return gameIsPlaying;
		}
	}

	private bool isPlaying = false;
	private bool gameIsPlaying = false;
	private bool isDemoMode = false;
	// Start is called before the first frame update
	void Start()
	{
	}
	public delegate void OnMoleHitDelegate(int points, Mole mole);
	public OnMoleHitDelegate onMoleHit;
	public delegate void OnMoleMissDelegate();
	public OnMoleMissDelegate onMoleMiss;

	// Update is called once per frame
	void Update()
	{
		if (clearMoles) {
			clearMoles = false;
			/*
			foreach(Mole m in moles) {
				if (m != null) RecycleMole(m);
			}
			*/
		}
		else if (isPlaying) {
			float dt = Time.deltaTime;
			timePassed += dt;
			float newSpeed = Mathf.Floor(timePassed / 20) + 1;
			if (!isDemoMode && newSpeed != speed) {
				Debug.Log("New Speed " + newSpeed);
				speed = newSpeed;
			}
			if (moles.Count < 3 && RandomHelper.PercentCheck(5)) {
				AddMole();
			}
			moles.ForEach(mm => mm.RunUpdate(dt));
			Mole m = null;
			for (int i=moles.Count-1; i>=0; i--) {
				m = moles[i];
				if (m != null && m.IsEnded()) {
					if (!m.WasHit) {
						onMoleMiss?.Invoke();
					}
					RecycleMole(m);
				}
			}
		}
	}

	public void PlayDemo() {
		Reset();
		isDemoMode = true;
		isPlaying = true;
	}

	public void Play() {
		Reset();
		isPlaying = true;
		gameIsPlaying = true;
	}

	public void Pause() {
		isPlaying = false;
	}
	public void UnPause() {
		isPlaying = true;
	}

	public void EndGame() {
		isPlaying = false;
		gameIsPlaying = false;
	}

	void Reset() {
		speed = 1;
		timePassed = 0;
		isPlaying = false;
		isDemoMode = false;
		clearMoles = true;
		gameIsPlaying = false;
	}

	void AddMole() {
		Mole m = MolePool.Instance.GetMole();
		Hole h = GetRandomEmptyHole();
		if (h) {
			m.SetSpeed(speed);
			m.onHit = OnMoleHit;
			h.AddMole(m);
			moles.Add(m);
		}
	}
	void RecycleMole(Mole m) {
		moles.Remove(m);
		Debug.Log("COUNT" + moles.Count);
		MolePool.Instance.Recycle(m);
	}

	Hole GetRandomEmptyHole() {
		List<Hole> tmp = new List<Hole>();
		foreach (Hole h in holes) {
			if (h != null && h.IsEmpty()) tmp.Add(h);
		}
		if (tmp.Count == 0) return null;
		return RandomHelper.GetListElement<Hole>(tmp);
	}

	void OnMoleHit(int points, Mole mole) {
		if (!isDemoMode) {
			onMoleHit?.Invoke(points, mole);
			if (isPlaying) mole.RunHit();
		}
	}
}
