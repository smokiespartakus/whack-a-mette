using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

	//public List<Hole> holes;

	public List<Mole> moles;
	public MoleSprite moleSpritePrefab;

	[SerializeField]
	private float level = 1;
	[SerializeField]
	
	private float timePassed = 0;
	private bool clearMoles;
	List<Mole> activeMoles = new List<Mole>();
	
	public bool IsPlaying {
		get {
			return gameIsPlaying;
		}
	}

	private bool isPlaying = false;
	private bool gameIsPlaying = false;
	private bool isDemoMode = false;
	// Start is called before the first frame update
	public delegate void OnMoleHitDelegate(int points, Mole mole);
	public OnMoleHitDelegate onMoleHit;
	public delegate void OnMoleMissDelegate();
	public OnMoleMissDelegate onMoleMiss;

	void OnEnable() {
		foreach (Mole m in moles) {
			m.Reset();
			m.gameObject.SetActive(false);
		}
	}

	void Start() {
		foreach (Mole m in moles) {
			MoleSprite spr = Instantiate(moleSpritePrefab);
			m.SetSprite(spr);
			spr.transform.localPosition = new Vector3(0f, 0.001f, 0f);
			spr.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
			m.Reset();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (clearMoles) {
			clearMoles = false;
			foreach (Mole m in moles) {
				m.Reset();
			}
			activeMoles.Clear();
		}
		else if (isPlaying) {
			float dt = Time.deltaTime;
			timePassed += dt;
			float newLevel = Mathf.Floor(timePassed / 20) + 1;
			if (!isDemoMode && newLevel != level) {
				// Debug.Log("New Level " + newLevel);
				level = newLevel;
			}
			if (RandomHelper.PercentCheck(5)) {
				Debug.Log("TRY SHOW MOLE; active=" + activeMoles.Count);
				if (activeMoles.Count < 3) {
					ShowMole();
				}
			}
			Mole m = null;
			for (int i=activeMoles.Count-1; i>=0; i--) {
				m = activeMoles[i];
				if (m != null && m.IsEnded()) {
					if (!m.WasHit && !isDemoMode) {
						onMoleMiss?.Invoke();
					}
				//	Debug.Log("ENDED " + m.name);
					//m.gameObject.SetActive(false);
					m.Reset();
					activeMoles.RemoveAt(i);
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
		level = 1;
		timePassed = 0;
		isPlaying = false;
		isDemoMode = false;
		clearMoles = true;
		gameIsPlaying = false;
	}

	void ShowMole() {
		Mole m = RandomHelper.GetListElement<Mole>(moles.FindAll(x => !x.IsActive()));
		if (m) {
			m.SetLevel(level);
			m.onHit = OnMoleHit;
			m.SetRandomLogo();
			activeMoles.Add(m);
			m.Show();
		}
	}

	Mole GetRandomInactiveMole() {
		List<Mole> tmp =  new List<Mole>();
		foreach (Mole m in moles) {
			if (m != null && !m.IsActive()) tmp.Add(m);
		}
		if (tmp.Count == 0) return null;
		return RandomHelper.GetListElement<Mole>(tmp);
	}

	void OnMoleHit(int points, Mole mole) {
		if (!isDemoMode) {
			onMoleHit?.Invoke(points, mole);
			if (isPlaying) mole.RunHit();
		}
	}
}
