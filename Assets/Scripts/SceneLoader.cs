using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	public void SceneLoad (string SceneName) {
		SceneManager.LoadScene (SceneName);
	}
}