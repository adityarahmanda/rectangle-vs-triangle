using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	public int health;
	public int damage;
	public float jumpForce;
	public float moveSpeed;
	private float moveInput;

	public KeyCode rightInput;
	public KeyCode leftInput;
	public KeyCode jumpInput;
	public KeyCode hitButton;
	public bool isReverse;

	private bool isCooldown;
	public float cooldownLenght;
	private float cooldownTime;

	public bool isAttack;
	public bool isGrounded;
	public Transform feetPos;
	public float checkRadius;

	public LayerMask whatIsGround;
	private Animator anim;

	public Transform enemyPos;
	private SpriteRenderer sprite;
	private IEnumerator coroutine;
	private bool isDead;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		coroutine = Flashing ();
	}

	void FixedUpdate() {
		if (!isDead) {
			rb.velocity = new Vector2 (moveInput * moveSpeed, rb.velocity.y);

			if (Input.GetKey (rightInput)) {
				moveInput = 1;
			} else if (Input.GetKey (leftInput)) {
				moveInput = -1;
			} else if (!Input.GetKey (rightInput) && !Input.GetKey (leftInput)) {
				moveInput = 0;
			}
		}
	}

	void Update() {
		if (enemyPos == null) {
			enemyPos = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Transform> ();
		}

		isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);

		//Mirror character when move ;eft or right
		if (!isDead) {
			if (!isReverse) {
				if (Input.GetKey (rightInput)) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
				} else if (Input.GetKey (leftInput)) {
					transform.eulerAngles = new Vector3 (0, 180, 0);
				}
			} else {
				if (Input.GetKey (rightInput)) {
					transform.eulerAngles = new Vector3 (0, 180, 0);
				} else if (Input.GetKey (leftInput)) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
				}
			}

			//jump code
			if (isGrounded && Input.GetKeyDown (jumpInput)) {
				rb.velocity = Vector2.up * jumpForce;
				GameManager.PlaySFX ("jump");
			}

			if (!isGrounded && Input.GetKeyDown (hitButton)) {
				isAttack = true;
				rb.velocity = Vector2.down * jumpForce;
				GameManager.PlaySFX ("hit");
			}

			if (isAttack && !GameManager.isBattleEnd && transform.position.y > enemyPos.position.y) {
				Physics2D.IgnoreLayerCollision (9, 10, false);
			}

			if (isGrounded && isAttack) {
				Physics2D.IgnoreLayerCollision (9, 10, true);
				isAttack = false;
				attackAnimation ();
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

		//player cooldown
		isCooldown = true;
		cooldownTime = cooldownLenght;
		StartCoroutine (coroutine);
	}

	public int getHealth() {
		return health;
	}

	public int GetDamage() {
		return damage;
	}

	public void attackAnimation() {
		anim.SetTrigger ("Attack");
	}

	public bool IsAttack {
		get { return isAttack; }
		set { isAttack = value; }
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