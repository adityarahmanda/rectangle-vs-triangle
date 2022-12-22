using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	private PlayerController player;
	private EnemyController enemy;

	public Sprite winStatusImage;
	public Sprite loseStatusImage;

	public Image gameStatus;
	public Image retryButton;
	public Image homeButton;
	
	void Start () {
		GameManager.isBattleEnd = false;
		gameStatus.color = Color.clear;
		retryButton.color = Color.clear;
		homeButton.color = Color.clear;
	}

	// Update is called once per frame
	void Update () {
		if (player == null && enemy == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
			enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyController> ();
		} else {
			if (!GameManager.isBattleEnd) {
				if (player.health <= 0) {
					gameStatus.sprite = loseStatusImage;
					GameManager.isBattleEnd = true;
				} 

				if (enemy.health <= 0) {
					gameStatus.sprite = winStatusImage;
					GameManager.isBattleEnd = true;
				}
			} else {
				gameStatus.color = Color.white;
				retryButton.color = Color.white;
				homeButton.color = Color.white;
				Physics2D.IgnoreLayerCollision (9, 10, true);
			}
		}
	}
}
