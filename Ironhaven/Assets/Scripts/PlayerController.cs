using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool facingRight;
	public bool jump;
	public bool attacking;
	public bool hurt;

	public float knockbackForce; //force of knockback
	public float maxRunSpeed; //maximum run speed of character
	public float runSpeed; //current runspeed so that it can be adjusted on the fly without overwriting the normal speed
	public float jumpForce; //jumping force of character
	public Transform groundCheck; //checks if grounded
	public float basicAttackTime; //Duration of basic attack
	public float basicAttackTimerCountdown; //Timer which prevents character for mashing the attack button
	public float hurtTime;
	public float hurtTimerCountdown;
	public float attackCooldown;
	public float attackCooldownTimer;

	private Animator animator;

	private Transform testHitBox; //***TEST FOR ATTACKING HITBOX***
	public Material baseMaterial;
	public Material damageMaterial;
	//private MeshRenderer playerMesh;

	public bool grounded;
	public Rigidbody2D rb2d;
	private EnemyController enemy;
	private EnemyHealth enemyHP;

	public float velocity;

	public Inventory inventory; // Making a slot to hold the inventory script for use in getting the inventory array that holds the weapons.

	// Use this for initialization
	void Awake ()
	{
		facingRight = true;
		jump = false;
		attacking = false;
		knockbackForce = 300f;
		maxRunSpeed = 10f;
		runSpeed = maxRunSpeed;
		grounded = false;
		jumpForce = 500f;
		basicAttackTime = 0.5f;
		basicAttackTimerCountdown = 0f;
		hurtTime = 0.5f;
		hurtTimerCountdown = 0f;
		attackCooldown = 0.5f;
		attackCooldownTimer = 0;
		//playerMesh = GetComponent<MeshRenderer> ();

		inventory = gameObject.GetComponent<Inventory> ();
		animator = gameObject.GetComponent<Animator> ();

		rb2d = GetComponent<Rigidbody2D> ();
		testHitBox = this.transform.Find("AttackBox"); //***TEST FOR ATTACKING HITBOX***
		testHitBox.gameObject.SetActive (false); //Makes hitbox (displayed red) not appear on startup
	}
	
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		checkHurt (); //checks if player is hurt
		if (Input.GetButtonDown ("Jump") && grounded && !hurt) //only allows jumping while the player is on the ground and the jump button is pressed, so no mid-air jumps are done
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

		if (Input.GetButtonDown ("Swap") && (inventory.inventory [0] != null && inventory.inventory [1] != null))
		{ //Doing the swap only if there are two weapons in the inventory. 
			inventory.switchWeapons (inventory.inventory [0], inventory.inventory [1]);
		}


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

		if ((Input.GetKey ("right") || Input.GetKey ("left")) && !hurt && grounded) //player's walk animation function is called if not hurt and is on the ground
		{
			setWalk ();
		}
		else
		{
			setIdle ();
		}

		if (jump) //only adds jumpforce when the jump button is initially pressed
		{
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
			setJump ();
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

	void countdownTimer() //timer decrement
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

	void setIdle() //function that triggers the idle animation
	{
		animator.SetTrigger ("playerIdle");
	}

	void setWalk() //function that triggers the walk animation
	{
		animator.SetTrigger ("playerWalk");
	}

	public void setHurt() //function that triggers the hurt animation, public so enemies call it when they hit player
	{
		animator.SetTrigger ("playerHit");
	}

	void setJump() //function that triggers the jump animation
	{
		animator.SetTrigger ("playerJump");
	}

	public void setDeath() //function that triggers the death animation, public so enemies call it when they hit player
	{
		animator.SetTrigger ("playerDeath");
	}

	void checkHurt() //checks if hurt
	{
		if (hurt) //player is red while hurt
		{
			//playerMesh.material = damageMaterial;
			runSpeed = 0;
		}
		else //normal colors otherwise
		{
			//playerMesh.material = baseMaterial;
			runSpeed = maxRunSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D other) //detects collision
	{
		if (other.gameObject.layer == 11) //if player's active hitbox hits enemy
		{
			//Debug.Log ("Player: Hit an enemy!");
			enemy = other.gameObject.GetComponent<EnemyController>();
			enemyHP = other.gameObject.GetComponent<EnemyHealth> ();
			if (inventory.inventory [0] != null)
			{
				if (inventory.inventory[0].name == "Bone Saw")
				{
					enemyHP.addDamage (5);
				}
				else if (inventory.inventory[0].name == "Knife") {
					enemyHP.addDamage (3);
				}
				else if (inventory.inventory[0].name == "Scalpel")
				{
					enemyHP.addDamage (2);
				}
				else if(inventory.inventory[0].name == "Syringe")
				{
					enemyHP.addDamage (0);
					enemy.startPoison ();
				}
			}
			else
			{
				enemyHP.addDamage (1);
			}

			Vector3 dir = other.transform.position - transform.position;
			dir.y = 0;

			if (other.attachedRigidbody) //knocks enemy back
			{
				other.attachedRigidbody.AddForce(dir.normalized * enemy.knockbackForce);
			}
		}
		if (other.gameObject.tag == "EnemyWeapon" && !hurt) //if enemy's active hitbox touches player
		{
			//Debug.Log ("Player: Hit by the enemy!");
			hurt = true;
			hurtTimerCountdown = hurtTime;
		}
	}
}
