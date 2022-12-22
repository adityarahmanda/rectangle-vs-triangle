using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStomper : MonoBehaviour {

	public PlayerController player;
	public EnemyController enemy;

	void Update() {
		if (enemy == null) {
			enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyController> ();
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			if (!enemy.IsCooldown) {
				if (transform.position.y > enemy.transform.position.y && player.isAttack && !enemy.IsDead) {
					enemy.Attacked (player.GetDamage ());
					Physics2D.IgnoreLayerCollision (9, 10, true);
					player.IsAttack = false;
				}
			} else {
				Physics2D.IgnoreLayerCollision (9, 10, true);
			}
		}
	}
}
