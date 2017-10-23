using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public bool test = false;

	private Animator animator;
	private GameObject player;
	public string enemyName; //stores name of enemy
	public float enemyMaxSpeed; //stores enemy max speed
	public float enemySpeed;
	public bool isHurt; //checks if enemy is hurt
	public float range; //stores enemy's line of sight
	public bool inRange; //is true if player is in enemy's range
	public float attackRange; //how close the enemy must be to player to attack
	public bool inAttackRange; //is true if player is in range of enemy's attack
	public bool isFacingRight = true;
	private Rigidbody2D rb2d;
	private BoxCollider2D b2d;
	public float spawnX; //stores x coordinate of spawn
	public float spawnY; //stores y coordinate of spawn
	public float patrolMin; //stores x coordinate of left bound of patrol
	public float patrolMax; //stores x coordinate of right bound of patrol
	public float patrolRange; //stores how far an enemy is patrolling
	public Vector3 patrolLeftEnd; //left bound of patrol
	public Vector3 patrolRightEnd; //right bound of patrol
	public bool equalX; //checks if player and enemy are aligned horizontally
	public bool equalY; //checks if player and enemy are aligned vertically
	public Vector3 targetOnDifferentY; //for grounded enemies so they don't constantly try to jump awkwardly at player

	// Use this for initialization
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		inRange = false; //checks if enemy is detected
		inAttackRange = false; //checks if enemy is in attacking range
		rb2d = GetComponent<Rigidbody2D>();
		b2d = GetComponent<BoxCollider2D> ();
		setName (); //sets name of enemy
		setStats (); //sets stats of enemy
		spawnX = transform.position.x; //gets x coordinate of spawn
		spawnY = transform.position.y; //gets y coordinate of spawn
		patrolMin = spawnX - patrolRange;
		patrolMax = spawnX + patrolRange;
		patrolLeftEnd = new Vector3 (patrolMin, transform.position.y, 0); //left bound of patrol area
		patrolRightEnd = new Vector3 (patrolMax, transform.position.y, 0); //right bound of patrol area
		isHurt = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player == null) //locates player object
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		rangeDetect (); //checks if player is in range
		checkVertical (); //checks if player is vertically aligned with enemy
		checkHorizontal (); //checks if player is horizontally aligned with enemy
		if (!isHurt) //cannot do anything while hurt
		{
			if (inRange) //if player is in range, then action is taken
			{
				enemyFight ();
			}
			else //patrols if player is not in range
			{
				patrol ();
			}
		}
	}

	void setName() //sets the name of enemy
	{
		if (this.tag.Equals ("Ghost"))
		{
			enemyName = "Ghost";
		}
	}

	void setStats() //sets traits of enemy
	{
		if (enemyName == "Ghost")
		{
			range = 10f;
			attackRange = 1f;
			enemyMaxSpeed = 2f;
			enemySpeed = enemyMaxSpeed;
			patrolRange = 2f;
		}
	}

	void rangeDetect() //checks if player is in range
	{
		if (Vector3.Distance (transform.position, player.transform.position) < range) { //enemy detection
			if (!inRange) {
				inRange = true;
			}
		}
		else
		{
			inRange = false;
		}
		if (Vector3.Distance (transform.position, player.transform.position) < attackRange) { //checks if enemy is in Attack Range
			if (!inAttackRange) {
				inAttackRange = true;
			}
		}
		else
		{
			inAttackRange = false;
		}
	}

	void checkVertical() //checks if enemy and player are vertically aligned
	{
		if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 0.5f)
		{
			equalX = true;
		}
		else
		{
			equalX = false;
		}
	}

	void checkHorizontal() //checks if enemy and player are horizontally aligned
	{
		if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.5f)
		{
			equalY = true;
		}
		else
		{
			equalY = false;
		}
	}

	void patrol() //patrol function
	{
		if (transform.position.x <= patrolMin || (isFacingRight && transform.position.x < patrolMax)) // enemy is past left bound or they are in bounds facing the right bound
		{
			if (!isFacingRight)
			{
				Flip ();
			}
			move (transform.position, patrolRightEnd);
		}
		else if(transform.position.x >= patrolMax || (!isFacingRight && transform.position.x > patrolMin)) //enemy is past right bound or they are in bounds facing left bound
		{
			if (isFacingRight)
			{
				Flip ();
			}
			move (transform.position, patrolLeftEnd);
		}
	}

	void enemyFight() //combat function
	{
		if (inAttackRange) //code for attack
		{
			
		}
		else //approaches player while they are in range
		{
			if (player.transform.position.x < transform.position.x && isFacingRight)
			{
				Flip ();
			}
			else if(player.transform.position.x > transform.position.x && !isFacingRight)
			{
				Flip ();
			}
			moveTowardsPlayer ();
		}
	}

	void move(Vector3 start, Vector3 end) //moves enemy in desired direction
	{
		float step = enemySpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (start, end, step);
	}

	void moveTowardsPlayer() //moves towards player
	{
		if (enemyName != "Ghost") //enemies that cannot float or move through the air can only move along the ground
		{
			targetOnDifferentY = new Vector3 (player.transform.position.x, spawnY, 0);
			move (transform.position, targetOnDifferentY);
		}
		else //floating enemies can move directly toward the player
		{
			move (transform.position, player.transform.position);
		}
	}

	void checkHurt()
	{
		if (isHurt)
		{
			enemySpeed = 0;
		}
		else
		{
			enemySpeed = enemyMaxSpeed;
		}
	}

	void Flip() //flip function
	{
		if(!isHurt) //cannot flip if hurt
		{
			isFacingRight = !isFacingRight;
			Vector3 enemyScale = transform.localScale;
			enemyScale.x *= -1;
			transform.localScale = enemyScale;
		}
	}
}
