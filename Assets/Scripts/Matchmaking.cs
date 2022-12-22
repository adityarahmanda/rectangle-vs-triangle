using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matchmaking : MonoBehaviour {

	public int enemyAttack;
	public float enemySpeed;

	public Image playerImage;
	public Text playerStatus;
	public RectTransform playerImageTransform;

	public Image enemyImage;
	public Text enemyStatus;
	public RectTransform enemyImageTransform;

	public Sprite rectangle;
	public Sprite triangle;
	public string characterChosen;

	public Image sceneTitle;
	public RectTransform sceneTitleTransform;
	public Sprite matchmakingImage;
	public Sprite matchImage;

	public SceneLoader sceneLoader;

	private bool isChoosePlayer;
	private bool isMatchMaking;
	private bool isMatch;

	public Image leftArrow;
	public Image rightArrow;

	void Start() {
		characterChosen = "Rectangle";
		isChoosePlayer = true;
		isMatchMaking = false;
		isMatch = false;
		leftArrow.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
		if (isChoosePlayer) {
			if (characterChosen == "Rectangle") {
				//Set player UI
				GameManager.playerCharacter= "Rectangle";
				playerImage.sprite = rectangle;
				playerImageTransform.eulerAngles = new Vector3 (0, 0, 0);
				playerStatus.text = "Name : Rectangle\nAttack : 30\nSpeed : 5";

				//Set enemy UI
				enemyImage.sprite = triangle;
				enemyImageTransform.eulerAngles = new Vector3 (0, 0, 0);
				enemyStatus.text = "Name : Triangle\nAttack : ?\nSpeed : ?";
			} else if (characterChosen == "Triangle") {
				//Set player UI
				GameManager.playerCharacter= "Triangle";
				playerImage.sprite = triangle;
				playerStatus.text = "Name : Triangle\nAttack : 20\nSpeed : 7.5";
				playerImageTransform.eulerAngles = new Vector3 (0, 180, 0);

				//Set enemy UI
				enemyImage.sprite = rectangle;
				enemyStatus.text = "Name : Rectangle\nAttack : ?\nSpeed : ?";
				enemyImageTransform.eulerAngles = new Vector3 (0, 180, 0);
			}
		} 

		if (isMatchMaking) {
			sceneTitle.sprite = matchmakingImage;

			//Set enemy status
			if (!isMatch) {
				if (characterChosen == "Rectangle") {
					enemyAttack = Random.Range (15, 30);
					enemySpeed = Random.Range (5, 10) + 0.5f;

					if ((enemyAttack == 20 && enemySpeed == 7.5f) || (enemyAttack == 25 && enemySpeed ==  6.5f)) {
						GameManager.enemyAttackStatus = enemyAttack;
						GameManager.enemySpeedStatus = enemySpeed;
						isMatch = true;
					}
				} else if (characterChosen == "Triangle") {
					enemyAttack = Random.Range (25, 40);
					enemySpeed = Random.Range (3, 8);

					if ((enemyAttack == 30 && enemySpeed == 5) || (enemyAttack == 35 && enemySpeed ==  3)) {
						GameManager.enemyAttackStatus = enemyAttack;
						GameManager.enemySpeedStatus = enemySpeed;
						isMatch = true;
					}
				}
			} else {
				sceneTitle.GetComponent<RectTransform> ().sizeDelta = new Vector2 (125, 100);
				sceneTitle.sprite = matchImage;
				StartCoroutine (ChangeScene());
			}
			enemyStatus.text = "Name : Rectangle\nAttack : " + enemyAttack + "\nSpeed : " + enemySpeed;
		}
	}

	public void ChooseCharacter(string value){
		characterChosen = value;
	}

	public void ToggleArrow(){
		if (leftArrow.color == Color.white) {
			leftArrow.color = Color.clear;
			rightArrow.color = Color.white;
		} else if (rightArrow.color == Color.white) {
			leftArrow.color = Color.white;
			rightArrow.color = Color.clear;
		}
	}

	public void CharacterChosen(){
		isChoosePlayer = false;
		isMatchMaking = true;
	}

	IEnumerator ChangeScene(){
			yield return new WaitForSeconds (2);
			sceneLoader.SceneLoad ("BattleScene");
	}
}