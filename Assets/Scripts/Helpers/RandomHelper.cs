using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHelper
{
	public static int GetIntXToY(int x, int y) {
		return Random.Range(1, y + 1);
	}

	public static float GetFloatXToY(float x, float y) {
		return Random.Range(x, y);
	}

	public static int GetInt0ToX(int x) {
		return GetIntXToY(0, x);
	}
	
	public static int GetInt1ToX(int x) {
		return GetIntXToY(1, x);
	}

	public static bool PercentCheck(int percent) {
		return GetInt1ToX(100) <= percent;
	}
	
	public static T GetListElement<T>(List<T> list) {
		if (list.Count == 0) return default(T);
		try {
			return list[GetInt0ToX(list.Count - 1)];
		} catch(System.Exception) {
			return default(T);
		}
	}
}
