using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool facingRight;
	public bool jump;
	public bool attacking;
	public bool hurt;

	public float moveForce;
	public float maxRunSpeed;
	public float runSpeed;
	public float jumpForce;
	public Transform groundCheck;
	public float basicAttackTime; //Duration of basic attack
	public float basicAttackTimerCountdown; //Timer which prevents character for mashing the attack button
	public float hurtTime;
	public float hurtTimerCountdown;
	public float attackCooldown;
	public float attackCooldownTimer;

	private Transform testHitBox; //***TEST FOR ATTACKING HITBOX***

	public bool grounded = false;
	public Rigidbody2D rb2d;
	private EnemyHealth enemyHP;

	public float velocity;

	// Use this for initialization
	void Awake ()
	{
		facingRight = true;
		jump = false;
		attacking = false;
		moveForce = 200f;
		maxRunSpeed = 10f;
		runSpeed = maxRunSpeed;
		jumpForce = 500f;
		basicAttackTime = 0.5f;
		basicAttackTimerCountdown = 0f;
		hurtTime = 0.5f;
		hurtTimerCountdown = 0f;
		attackCooldown = 0.5f;
		attackCooldownTimer = 0;

		rb2d = GetComponent<Rigidbody2D> ();
		testHitBox = this.transform.Find("AttackBox"); //***TEST FOR ATTACKING HITBOX***
		testHitBox.gameObject.SetActive (false); //Makes hitbox (displayed red) not appear on startup
	}
	
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		checkHurt ();
		if (Input.GetButtonDown ("Jump") && grounded) //only allows jumping while the player is on the ground and the jump button is pressed, so no mid-air jumps are done
		{
			jump = true;
		}
		velocity = rb2d.velocity.x;

		if(Input.GetKeyDown("z") && !attacking && !hurt) //code for basic attacking, cannot attack if already in the middle of an attacking and cannot attack if hurt
		{
			attacking = true;
			basicAttackTimerCountdown = basicAttackTime;
			attackCooldownTimer = attackCooldown;
			testHitBox.gameObject.SetActive (true); //visually turns on hitbox
		}

		countdownTimer ();
	}

	void FixedUpdate()
	{
		if(Input.GetKey("right")) //moves player right
		{
			if (!facingRight) //flips player if they are facing left
			{
				Flip ();
			}

			transform.Translate (runSpeed * Time.deltaTime, 0.0f, 0.0f); //moves a set distance
		}
		if(Input.GetKey("left")) //moves player left
		{
			if (facingRight) //flips player if they are facing right
			{
				Flip ();
			}
			transform.Translate (runSpeed * Time.deltaTime * -1, 0.0f, 0.0f);
		}

		if (jump) //only adds jumpforce when the jump button is initially pressed
		{
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	public void damage()
	{
		hurt = true;
		hurtTimerCountdown = hurtTime;
	}

	void Flip() //flips character sprite
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void countdownTimer()
	{
		if (basicAttackTimerCountdown > 0) //ensures attack goes for specific duration
		{
			basicAttackTimerCountdown -= Time.deltaTime;
		}
		if (basicAttackTimerCountdown <= 0) //ensures attack goes for specific duration
		{
			attacking = false;
			testHitBox.gameObject.SetActive (false);
		}

		if (attackCooldownTimer > 0 && !attacking)
		{
			attackCooldownTimer -= Time.deltaTime;
		}

		if (hurtTimerCountdown > 0)
		{
			hurtTimerCountdown -= Time.deltaTime;
		}
		if (hurtTimerCountdown <= 0)
		{
			hurt = false;
		}
	}

	void checkHurt()
	{
		if (hurt)
		{
			runSpeed = 0;
		}
		else
		{
			runSpeed = maxRunSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D other) //detects collision
	{
		if (other.gameObject.layer == 11) //if player's active hitbox hits enemy
		{
			Debug.Log ("Player: Hit an enemy!");
			enemyHP = other.gameObject.GetComponent<EnemyHealth> ();
			enemyHP.addDamage (1);
		}
		if (other.gameObject.tag == "EnemyWeapon" && !hurt) //if enemy's active hitbox touches player
		{
			Debug.Log ("Player: Hit by the enemy!");
			hurt = true;
			hurtTimerCountdown = hurtTime;
		}
	}
}
