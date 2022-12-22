using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

	public GameManager gameManager;

	void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	public void Toggle(){
		gameManager.ToggleSound ();
	}
}
