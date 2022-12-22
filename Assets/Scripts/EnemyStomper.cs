using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomper : MonoBehaviour {

	public PlayerController player;
	public EnemyController enemy;

	void Update() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			if (!player.IsCooldown) {
				if (transform.position.y > player.transform.position.y && enemy.isAttack && !player.IsDead) {
					player.Attacked (enemy.GetDamage ());
					Physics2D.IgnoreLayerCollision (9, 10, true);
					enemy.isAttack = false;
				}
			} else {
				Physics2D.IgnoreLayerCollision (9, 10, true);
			}
		}
	}
}
