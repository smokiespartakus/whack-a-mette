using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

	public List<Hole> holes;
	public Mole mole;
	List<Mole> moles = new List<Mole>();
	
	private bool isPlaying = false;
	// Start is called before the first frame update
	void Start()
	{
		Play();	
	}

	// Update is called once per frame
	void Update()
	{
		if (isPlaying) {
			float dt = Time.deltaTime;
			if (moles.Count < 3 && RandomHelper.PercentCheck(5)) {
				AddMole();
			}
			moles.ForEach(mm => mm.RunUpdate(dt));
			Mole m = null;
			for (int i=moles.Count-1; i>=0; i--) {
				m = moles[i];
				if (m != null && m.IsEnded()) {
					moles.Remove(m);
					Debug.Log("COUNT" + moles.Count);
					MolePool.Instance.Recycle(m);
				}
			}
		}
	}

	void Play() {
		isPlaying = true;
	}

	void Pause() {
		isPlaying = false;
	}

	void AddMole() {
		Mole m = MolePool.Instance.GetMole();
		Hole h = GetRandomEmptyHole();
		if (h) {
			h.AddMole(m);
			moles.Add(m);
		}
	}

	Hole GetRandomEmptyHole() {
		List<Hole> tmp = new List<Hole>();
		foreach (Hole h in holes) {
			if (h != null && h.IsEmpty()) tmp.Add(h);
		}
		if (tmp.Count == 0) return null;
		return RandomHelper.GetListElement<Hole>(tmp);
	}
}
