using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool facingRight = true;
	public bool jump = false;
	public bool attacking = false;
	public bool hurt = false;

	public float moveForce = 200f;
	public float runSpeed = 10f;
	public float jumpForce = 500f;
	public Transform groundCheck;
	public float basicAttackTime = 0.5f; //Duration of basic attack
	public float basicAttackTimerCountdown = 0f; //Timer which prevents character for mashing the attack button
	public float hurtTime = 0.5f;
	public float hurtTimerCountdown = 0.5f;

	private Transform testHitBox; //***TEST FOR ATTACKING HITBOX***

	public bool grounded = false;
	public Rigidbody2D rb2d;

	public float velocity;

	// Use this for initialization
	void Awake ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		testHitBox = this.transform.Find("AttackBox"); //***TEST FOR ATTACKING HITBOX***
		testHitBox.gameObject.SetActive (false); //Makes hitbox (displayed red) not appear on startup
	}
	
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) //only allows jumping while the player is on the ground and the jump button is pressed, so no mid-air jumps are done
		{
			jump = true;
		}
		velocity = rb2d.velocity.x;

		if(Input.GetKeyDown("z") && !attacking && !hurt) //code for basic attacking, cannot attack if already in the middle of an attacking and cannot attack if hurt
		{
			attacking = true;
			basicAttackTimerCountdown = basicAttackTime;
			testHitBox.gameObject.SetActive (true); //visually turns on hitbox
		}
		if (attacking && basicAttackTimerCountdown > 0) //ensures attack goes for specific duration
		{
			basicAttackTimerCountdown -= Time.deltaTime;
		}
		if (basicAttackTimerCountdown <= 0) //ensures attack goes for specific duration
		{
			attacking = false;
			testHitBox.gameObject.SetActive (false);
		}

		if (hurt && hurtTimerCountdown > 0)
		{
			hurtTimerCountdown -= Time.deltaTime;
		}
		if (hurtTimerCountdown <= 0)
		{
			hurt = false;
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
}
