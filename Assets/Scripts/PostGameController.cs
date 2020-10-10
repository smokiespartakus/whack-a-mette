using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameController : MonoBehaviour
{
	public MainController mainCtrl;
	public TMPro.TextMeshProUGUI pointsText;
	public TMPro.TextMeshProUGUI newRecordText;
	public Button backButton;

	void OnEnable() {
		backButton.onClick.AddListener(BackClick);
		newRecordText.gameObject.SetActive(false);
		pointsText.gameObject.SetActive(false);
	}
	void OnDisable() {
		backButton.onClick.RemoveListener(BackClick);
	}
	public void Show(int finalScore, bool isRecord) {
		pointsText.text = finalScore + "!";
		pointsText.gameObject.SetActive(true);
		if (isRecord)
			newRecordText.gameObject.SetActive(true);
	}
	void BackClick() {
		mainCtrl.ShowMenu(true);
		mainCtrl.box.PlayDemo();
	}
}
