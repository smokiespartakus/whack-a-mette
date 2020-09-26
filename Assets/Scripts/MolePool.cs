using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolePool : MonoBehaviour
{
	private static MolePool _instance;
	public static MolePool Instance {
		get {
			if(_instance == null) {
				GameObject go = new GameObject();
				_instance = go.AddComponent<MolePool>();
			}
			return _instance;
		}
	}

	void Start() {
		if (_instance == null) {
			_instance = GetComponent<MolePool>();
		}
	}
	
	public Mole molePrefab;
	private List<Mole> pool = new List<Mole>();

	public Mole GetMole() {
		Mole m;
		if (pool.Count > 0) {
		   m = pool[0];
		   pool.RemoveAt(0);
		}  else {
			m = Instantiate(molePrefab);
			m.transform.SetParent(transform);
		}
		m.gameObject.SetActive(true);
		return m;
	}

	public void Recycle(Mole m) {
		if (pool.Count < 10) {
			m.transform.SetParent(transform);
			pool.Add(m);
			m.gameObject.SetActive(false);
		}
	}

}
