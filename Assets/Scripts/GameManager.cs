using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static GameManager instance = null;
	static AudioClip jump, hit, dead;
	static AudioSource audioSrc;

	static Scene currentScene;
	private string sceneName;

	public static string playerCharacter;
	public static int enemyAttackStatus;
	public static float enemySpeedStatus;

	public GameObject rectanglePrefab;
	public GameObject trianglePrefab;
	public GameObject enemyRectanglePrefab;
	public GameObject enemyTrianglePrefab;

	private Image soundButton;
	public Sprite muteMusicImage;
	public Sprite unmuteMusicImage;

	public static bool isBattleEnd;

	// Use this for initialization
	void Start(){
		audioSrc = GetComponent<AudioSource> ();
		jump = Resources.Load<AudioClip> ("jump");
		hit = Resources.Load<AudioClip> ("hit");
		dead = Resources.Load<AudioClip> ("dead");
	}

	void Update(){
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;

		if (sceneName == "MainMenu") {
			if (soundButton == null) {
				soundButton = GameObject.FindGameObjectWithTag ("SoundButton").GetComponent<Image> ();
			}
		} else if (sceneName == "BattleScene") {
			if (GameObject.FindGameObjectWithTag ("Player") == null && GameObject.FindGameObjectWithTag ("Enemy") == null) {
				if (playerCharacter == "Rectangle") {
					Instantiate (rectanglePrefab, rectanglePrefab.transform.position, rectanglePrefab.transform.rotation);
					Instantiate (enemyTrianglePrefab, enemyTrianglePrefab.transform.position, enemyTrianglePrefab.transform.rotation);
				} else if (playerCharacter == "Triangle") {
					Instantiate (trianglePrefab, trianglePrefab.transform.position, trianglePrefab.transform.rotation);
					Instantiate (enemyRectanglePrefab, enemyRectanglePrefab.transform.position, enemyRectanglePrefab.transform.rotation);
				}
			}
		}
	}

	private void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	public void ToggleSound() {
		if(audioSrc.volume == 0) {
			soundButton.sprite = unmuteMusicImage;
			audioSrc.volume = 1;
		} else {
			soundButton.sprite = muteMusicImage;
			audioSrc.volume = 0;
		}
	}

	public static void PlaySFX(string audioClip){
		switch (audioClip) {
		case "jump":
			audioSrc.PlayOneShot (jump);
			break;
		case "hit":
			audioSrc.PlayOneShot (hit);
			break;
		case "dead":
			audioSrc.PlayOneShot (dead);
			break;
		}
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
