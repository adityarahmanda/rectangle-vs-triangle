using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour {

	private float timer;
	public float minTime;
	public float maxTime;

	private Transform playerPos;
	private Transform enemyPos;
	private float speed;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		playerPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		enemyPos = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Transform> ();
		speed = GameManager.enemySpeedStatus;
		timer = Random.Range (minTime, maxTime);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (timer <= 0) {
			animator.SetTrigger ("attack");
		} else {
			timer -= Time.deltaTime;
		}

		if (animator.transform.position.x > playerPos.position.x) {
			enemyPos.eulerAngles = new Vector3 (0, 180, 0);
		} else {
			enemyPos.eulerAngles = new Vector3 (0, 0, 0);
		}
		Vector2 target = new Vector2 (playerPos.position.x, animator.transform.position.y);
		animator.transform.position = Vector2.MoveTowards (animator.transform.position, target, -speed * Time.deltaTime);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}
}
