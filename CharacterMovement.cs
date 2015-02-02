using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	//Reference to the gameController object
	private GameObject gameController;
	//Make the controls differ depending on which player it is
	public bool player1;
	//Is this character currently selected
	public bool activated;
	//Keep track of the number of moves left
	public int moves = 4;
	//Is this player currently moveable
	public bool moveable = false;
	//Has this player already committed to a movement? Basically, prevent direction change mid-move
	public bool controlable = false;
	//Movement control booleans
	private bool up = false;
	private bool down = false;
	private bool left = false;
	private bool right = false;
	//Movement speed
	public float speed = 1.0f;
	//A reference to the current grid square
	private GameObject grid;
	//Booleans to set if a move is legal from this grid square
	public bool legalUp;
	public bool legalDown;
	public bool legalLeft;
	public bool legalRight;
	//Dodge boolean
	public bool dodge;
	//Dodge speed
	public float dodgeSpeed = 2.0f;
	//Original speed
	public float standardSpeed;
	//Dodge timer
	public float iFrames;
	//Dodge cooldown
	public float dodgeCooldown;


	// Use this for initialization
	void Start () {
		//Remove this later
		moveable = true;
		speed = speed * Time.deltaTime;
		//***
//		gameController = GameObject.FindWithTag ("GameController");
//		gameController.GetComponent<GameController> ().initControllable ();
		legalUp = false;
		legalDown = false;
		legalLeft = false;
		legalRight = false;
		dodgeCooldown = 2.0f;
		dodgeSpeed = dodgeSpeed * Time.deltaTime;
		standardSpeed = speed;
		dodge = false;


	}
	
	// Update is called once per frame
	void Update () {
		handleControls ();
		if (dodgeCooldown > 0f) {
			dodge = false;
			dodgeCooldown -= Time.deltaTime;
			if (dodgeCooldown < 0.0f) {
				dodgeCooldown = 0.0f;
				//dodge = true;
			}
		}
		if (dodge) {
			speed = dodgeSpeed;
			iFrames += Time.deltaTime;
			if (iFrames >= 0.3f) {
				speed = standardSpeed;
				dodgeCooldown = 2.0f;
				iFrames = 0.0f;
				dodge = false;
			}
		}
		if (player1 && Input.GetKeyDown("q") && dodgeCooldown == 0.0f) {
			dodge = true;
		}
		if (!player1 && Input.GetKeyDown("[7]") && dodgeCooldown == 0.0f) {
			dodge = true;
		}
	}

	void handleControls() {
		if (activated) {
			if (controlable) {
				moveable = true;
				//Player 1 controls
				if (player1) {
					if (Input.GetKey("w") && controlable && legalUp) {
						up = true;
						controlable = false;
					}
					if (Input.GetKey("a") && controlable && legalLeft) {
						left = true;
						controlable = false;
					}
					if (Input.GetKey("s") && controlable && legalDown) {
						down = true;
						controlable = false;
					}
					if (Input.GetKey("d") && controlable && legalRight) {
						right = true;
						controlable = false;
					}
				}
				//Player 2 controls
				else {
					if (Input.GetKey("up") && controlable && legalUp) {
						up = true;
						controlable = false;
					}
					if (Input.GetKey("left") && controlable && legalLeft) {
						left = true;
						controlable = false;
					}
					if (Input.GetKey("down") && controlable && legalDown) {
						down = true;
						controlable = false;
					}
					if (Input.GetKey("right") && controlable && legalRight) {
						right = true; 
						controlable = false;
					}
				}
			}//End of controllable
		}//End of activated

		if (moveable) {
			if (up) {
				transform.Translate(Vector3.forward * speed);
			}
			if (down) {
				transform.Translate(Vector3.back * speed);
			}
			if (left) {
				transform.Translate(Vector3.left * speed);
			}
			if (right) {
				transform.Translate(Vector3.right * speed);
			}
		}

		if (!moveable) {
			up = false;
			down = false;
			left = false;
			right = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Grid") {
			//Debug.Log("Entered a grid square");
			grid = other.gameObject;
			legalUp = grid.GetComponent<GridScript>().up;
			legalDown = grid.GetComponent<GridScript>().down;
			legalLeft = grid.GetComponent<GridScript>().left;
			legalRight = grid.GetComponent<GridScript>().right;
			//Debug.Log ("Moves: U-" + legalUp + " D-" + legalDown + " L-" + legalLeft + " R-" + legalRight); 
		}
//		if (other.tag == "Bullet") {
//			Destroy (other.gameObject);
//		}
	}
}
