using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	public Transform playerPos;

	public int health;
	public int damage;

	public bool isAttack;
	private bool isGrounded;
	public Transform feetPos;
	public float checkRadius;

	private bool isCooldown;
	public float cooldownLenght;
	private float cooldownTime;

	public LayerMask whatIsGround;
	public Animator anim;

	public SpriteRenderer sprite;
	private IEnumerator coroutine;

	private bool isDead;

	void Start() {
		damage = GameManager.enemyAttackStatus;
		coroutine = Flashing ();
	}

	void Update(){
		if (playerPos == null) {
			playerPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		}

		isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);

		if (!isDead) {
			if (transform.position.x >= playerPos.position.x) {
				transform.eulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.eulerAngles = new Vector3 (0, 180, 0);
			}

			if (isAttack && !GameManager.isBattleEnd && transform.position.y > playerPos.position.y) {
				Physics2D.IgnoreLayerCollision (9, 10, false);
			}

			if (isGrounded && isAttack) {
				Physics2D.IgnoreLayerCollision (9, 10, true);
				isAttack = false;
			}

			if (health <= 0) {
				anim.SetBool ("isDead", true);
				StopCoroutine (coroutine);
				sprite.color = Color.white;
				isCooldown = false;
				isDead = true;
			}

			if (isCooldown) {
				if (cooldownTime <= 0) {
					Physics2D.IgnoreLayerCollision (0, 9, false);
					StopCoroutine (coroutine);
					sprite.color = Color.white;
					isCooldown = false;
				} else {
					cooldownTime -= Time.deltaTime;
				}
			}
		}
	}

	public void Attacked(int damage) {
		health -= damage;

		//enemy cooldown
		isCooldown = true;
		cooldownTime = cooldownLenght;
		StartCoroutine (coroutine);
	}

	public int GetDamage() {
		return damage;
	}

	public bool IsCooldown {
		get { return isCooldown; }
		set { isCooldown = value; }
	}

	public bool IsDead {
		get { return isDead; }
		set { isDead = value; }
	}

	IEnumerator Flashing(){
		while (true) {
			sprite.color = Color.clear;
			yield return new WaitForSeconds (0.1f);
			sprite.color = Color.white;
			yield return new WaitForSeconds (0.1f);
		}
	}
}
