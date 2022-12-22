using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	private Image healthImage;
	public PlayerController player;
	public Image healthFrameImage;
	public Sprite secondHealthFrameImage;

	void Start() {
		healthImage = GetComponent<Image> ();

		if (GameManager.playerCharacter == "Triangle") {
			healthFrameImage.sprite = secondHealthFrameImage;
		}
	}

	void Update() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}

		if(((float)player.health / 100.0f) < healthImage.fillAmount) {
			healthImage.fillAmount -= 0.01f;
		}
	}
}
