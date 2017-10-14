using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool facingRight = true;
	public bool jump = false;

	public float moveForce = 200f;
	public float maxSpeed = 3f;
	public float jumpForce = 500f;
	public Transform groundCheck;

	public bool grounded = false;
	public Rigidbody2D rb2d;

	public float velocity;

	// Use this for initialization
	void Awake ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded)
		{
			jump = true;
		}
		velocity = rb2d.velocity.x;
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");

		if (moveHorizontal * rb2d.velocity.x < maxSpeed)
		{
			rb2d.AddForce (Vector2.right * moveHorizontal * moveForce);
		}

		if(Mathf.Abs(rb2d.velocity.x) > maxSpeed)
		{
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		if (moveHorizontal > 0 && !facingRight)
		{
			Flip ();
		}
		else if (moveHorizontal < 0 && facingRight)
		{
			Flip ();
		}

		if (jump)
		{
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
