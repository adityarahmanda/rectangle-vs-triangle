using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour {

	private Transform playerPos;
	private float speed;
	private int decision;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		playerPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		speed = GameManager.enemySpeedStatus;
		decision = Random.Range (0, 3);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Vector2 target = new Vector2 (playerPos.position.x, animator.transform.position.y);
		animator.transform.position = Vector2.MoveTowards (animator.transform.position, target, speed * Time.deltaTime);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (decision == 0) {
			animator.SetTrigger ("idle");
		} else {
			animator.SetTrigger ("follow");
		}
	}
}
