using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public Button startButton;
	public Button continueButton;
	public Button quitButton;

	public TMPro.TextMeshProUGUI hsText;

	public GameController game;
	public MainController mainCtrl;
	// Start is called before the first frame update
	void OnEnable() {
		startButton.gameObject.SetActive(!mainCtrl.game.IsPlaying);
		continueButton.gameObject.SetActive(mainCtrl.game.IsPlaying);
		quitButton.gameObject.SetActive(mainCtrl.game.IsPlaying);
		startButton.onClick.AddListener(StartClick);
		continueButton.onClick.AddListener(ContinueClick);
		quitButton.onClick.AddListener(QuitClick);
		int hs = PlayerPrefHelper.GetHighscore();
		if (hs > 0) {
			hsText.text = "Highscore: " + hs;
		} else {
			hsText.text = "";
		}
	}
	void OnDisable() {
		startButton.onClick.RemoveListener(StartClick);
		continueButton.onClick.RemoveListener(ContinueClick);
		quitButton.onClick.RemoveListener(QuitClick);
	}

	void StartClick() {
		mainCtrl.ShowGame(true);
		mainCtrl.game.Play();
	}
	void ContinueClick() {
		mainCtrl.game.UnPause();
		mainCtrl.ShowGame(true);
	}
	void QuitClick() {
		mainCtrl.game.EndGame();
	}
}
