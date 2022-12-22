using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
	private Image healthImage;
	public EnemyController enemy;
	public Image healthFrameImage;
	public Sprite secondHealthFrameImage;

	void Start() {
		healthImage = GetComponent<Image> ();

		if (GameManager.playerCharacter == "Triangle") {
			healthFrameImage.sprite = secondHealthFrameImage;
		}
	}

	void Update() {
		if (enemy == null) {
			enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyController> ();
		}

		if(((float)enemy.health / 100.0f) < healthImage.fillAmount) {
			healthImage.fillAmount -= 0.01f;
		}
	}
}
