using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
	public MenuController menu;
	public GameController game;
	public PostGameController postGame;
	public Box box;
	void OnEnable() {
		ShowMenu(true);
	}

	void OnDisable() {
		game.Pause();
	}
	// Start is called before the first frame update
	void Start()
	{
		box.PlayDemo();
	}

	// Update is called once per frame
	public MainController ShowMenu(bool hideRest) {
		menu.gameObject.SetActive(true);
		if (hideRest) {
			game.gameObject.SetActive(false);
			postGame.gameObject.SetActive(false);
		}
		return this;
	}
	public MainController ShowPostGame(bool hideRest) {
		postGame.gameObject.SetActive(true);
		if (hideRest) {
			game.gameObject.SetActive(false);
			menu.gameObject.SetActive(false);
		}
		return this;
	}
	public MainController ShowGame(bool hideRest) {
		game.gameObject.SetActive(true);
		if (hideRest) {
			postGame.gameObject.SetActive(false);
			menu.gameObject.SetActive(false);
		}
		return this;	
	}
	public MainController HideMenu() {
		menu.gameObject.SetActive(false);
		return this;
	}
	public MainController HideGame() {
		game.gameObject.SetActive(false);
		return this;
	}
	public MainController HidePostGame() {
		postGame.gameObject.SetActive(false);
		return this;
	}
}
