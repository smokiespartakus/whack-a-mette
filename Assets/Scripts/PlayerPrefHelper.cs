using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefHelper
{

	public static bool UpdateHighscore(int score) {
		int hs = GetHighscore();
		Debug.Log("UPDATE HIGH " + hs + " " + score);
		if (score > hs) {
			PlayerPrefs.SetInt("HIGHSCORE", score);
			return true;
		}
		return false;
	}
	public static int GetHighscore() {
		return PlayerPrefs.GetInt("HIGHSCORE", 0);
	}
}
