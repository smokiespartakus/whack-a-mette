using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Box box;
	public TMPro.TextMeshProUGUI scoreText;
	public Button menuButton;
	public MainController mainCtrl;
	public bool IsPlaying {
		get{
			return box.IsPlaying;
		}
	}
	int score;
	int missedMoles;

	// Start is called before the first frame update
	void OnEnable() {
		box.onMoleHit += OnMoleHit;
		box.onMoleMiss += OnMoleMiss;
		menuButton.onClick.AddListener(MenuClick);
	}
	void OnDisable() {
		box.onMoleHit -= OnMoleHit;
		box.onMoleMiss -= OnMoleMiss;
		menuButton.onClick.RemoveListener(MenuClick);
	}
	public void Pause() {
		box.Pause();
	}
	public void Play() {
		Reset();
		box.Play();
	}

	public void UnPause() {
		box.UnPause();
	}
	public void EndGame() {
		// Save score to highscore
		box.EndGame();
		mainCtrl.ShowPostGame(true);
		bool newRecord = PlayerPrefHelper.UpdateHighscore(score);
		mainCtrl.postGame.Show(score, newRecord);
	}

	void OnMoleHit(int points, Mole mole) {
		score += points;
		UpdateScoreText();
	}
	void UpdateScoreText() {
		scoreText.text = "Score: " + score;
	}
	void OnMoleMiss() {
		Debug.Log("ON MOLE MUSS" + missedMoles);
		missedMoles++;
		if (missedMoles == 10) {
			EndGame();
		}
	}
	void MenuClick() {
		box.Pause();
		mainCtrl.ShowMenu(false);
	}
	void Reset() {
		score = 0;
		missedMoles = 0;
		UpdateScoreText();
	}
}
