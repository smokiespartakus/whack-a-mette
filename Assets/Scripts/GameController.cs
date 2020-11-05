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
	public Transform metteArm;
	public GameObject metteFull;
	public GameObject metteNoArm;

	public float armX = 0.28f;
	public float armY = 0.5f;
	public float armZ = 1f;

	public bool IsPlaying {
		get{
			return box.IsPlaying;
		}
	}
	int score;
	int missedMoles;

	int leanTweenId;
	Vector3 armHomePos;

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
	void Start() {
		metteFull.SetActive(true);
		metteNoArm.SetActive(false);
		metteArm.gameObject.SetActive(false);
		armHomePos = metteArm.position;
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
		FlyArm(mole);
	}
	void UpdateScoreText() {
		scoreText.text = "Score: " + score;
	}
	void OnMoleMiss() {
		//Debug.Log("ON MOLE MUSS" + missedMoles);
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
	void FlyArm(Mole mole) {
		LeanTween.cancel(metteArm.gameObject);
		metteArm.gameObject.SetActive(true);
		Vector3 mp = mole.transform.position;
		//metteArm.localRotation = Quaternion.Euler(0f,0f,105f);
		//metteArm.position = new Vector3(-1 * mp.x + 3.3f, -1 * mp.y + 5.7f, -1* mp.z - 0.2f);
		Debug.Log("POS " + mp);
		Vector3 newPos = new Vector3(mp.x + armX, mp.y + armY, mp.z + armZ);
		//metteArm.position = newPos;
		LeanTween.rotateLocal(metteArm.gameObject, new Vector3(0f, 0f, 105f), 0.2f);
		LTDescr tween = LeanTween.move(metteArm.gameObject, newPos, 0.2f);
		metteFull.SetActive(false);
		metteNoArm.SetActive(true);
		tween.setOnComplete(FlyArmHome);
	}
	void FlyArmHome() {
		LeanTween.cancel(metteArm.gameObject);
		LeanTween.rotateLocal(metteArm.gameObject, Vector3.zero, 0.2f);
		//metteArm.localRotation = Quaternion.Euler(0f,0f,0);
		LTDescr tween = LeanTween.move(metteArm.gameObject, armHomePos, 0.2f);
		tween.setOnComplete(() => {
			metteArm.gameObject.SetActive(false);
			metteFull.SetActive(true);
			metteNoArm.SetActive(false);
		});
	}
}
