using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBehaviour : StateMachineBehaviour {

	private int decision;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		decision = Random.Range (0, 2);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (decision == 0) {
			animator.SetTrigger ("idle");
		} else {
			animator.SetTrigger ("follow");
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
