using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {

	public Rigidbody bullet;
	public float speed = 3.0f;
	private int shootDir = 0; //0 if not shooting
	private bool shootable;
	private float timer;
	//detect which player this instance is attached to, for controls
	public bool player1;
	//A reference to the mouse mover object
	public GameObject mouseController;
	//Mouse target position
	Vector3 targetPos;
	//AudioSource for paintball sounds
	public AudioClip paintballShot;
	//For continuous suppressing fire
	public bool suppressingFire;
	//Timer used for the continuous fire method.
	public float thisTimer;
	//Store a reference to the previous shooting direction
	public int oldShootDir;
	
	// Use this for initialization
	void Start () {
		speed = speed * Time.deltaTime;
		shootable = true;
		timer = 0.0f;
		player1 = this.GetComponent<CharacterMovement> ().player1;
		mouseController = GameObject.FindWithTag ("MouseControl");
		suppressingFire = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (player1 && Input.GetKeyDown ("e")) {
			suppressingFire = true;
			Debug.Log ("FUCKING SUPRESS");
		}
		else if (!player1 && Input.GetKeyDown ("[9]")) {
			suppressingFire = true;
		}
		if (suppressingFire && oldShootDir == 0) {
			Debug.Log ("OldshootDir is zero");
			suppressingFire = false;
		}
		else if (suppressingFire) {
			continuousFire(oldShootDir);
		}
		else {
			if (this.GetComponent<CharacterMovement> ().activated) { //Formerly: if (this.GetComponent<CharacterMovement> ().controlable) {
				//Player 1 controls
				if (player1) {
					if (Input.GetMouseButton(1)) {
						targetPos = mouseController.transform.position - transform.position;
						targetPos.Normalize();
						shootDir = 5;
					}
					else if (Input.GetKey("i")) { //change this back to if, if the mouse stuff is broken
						shootDir = 1;
						oldShootDir = shootDir;
					}
					else if (Input.GetKey("k")) {
						shootDir = 2;
					}
					else if (Input.GetKey("j")) {
						shootDir = 3;
						oldShootDir = shootDir;
					}
					else if (Input.GetKey("l")) {
						shootDir = 4;
						oldShootDir = shootDir;
					}
					else {
						shootDir = 0;
					}
				}
				//Player 2 controls
				else {
					if (Input.GetKey("[8]")) {
						shootDir = 1;
						oldShootDir = shootDir;
					}
					else if (Input.GetKey("[5]")) {
						shootDir = 2;
						oldShootDir = shootDir;
					}
					else if (Input.GetKey("[4]")) {
						shootDir = 3;
						oldShootDir = shootDir;
					}
					else if (Input.GetKey("[6]")) {
						shootDir = 4;
						oldShootDir = shootDir;
					}
					else {
						shootDir = 0;
					}
				}
			}//end of if controlable


			if (shootable) {
				if (shootDir != 0) {
					shootable = false;
					shootBullet (shootDir);
				}
			}
			if (!shootable) {
				timer += Time.deltaTime;
				if (timer >= (5f*Time.deltaTime)) {
					timer = 0.0f;
					shootable = true;
				}
			}
		}//end of else

	}

	void shootBullet(int dir) {
		Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, transform.position, transform.rotation);
		bulletClone.GetComponent<BulletScript> ().shootDir = dir;
		bulletClone.GetComponent<BulletScript> ().speed = speed;
		bulletClone.GetComponent<BulletScript> ().player1 = this.GetComponent<CharacterMovement> ().player1;
		//Handle the mouse crap
		if (shootDir == 5) {
			bulletClone.GetComponent<BulletScript> ().mouseTarget = targetPos;
		}
		AudioSource.PlayClipAtPoint(paintballShot, transform.position);
	}

	void continuousFire(int dir) {
		Debug.Log ("We made it");
		thisTimer = 0.0f;
		while (true) {
			if (thisTimer == 0.0f) {
				Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, transform.position, transform.rotation);
				bulletClone.GetComponent<BulletScript> ().shootDir = dir;
				bulletClone.GetComponent<BulletScript> ().speed = speed;
				bulletClone.GetComponent<BulletScript> ().player1 = this.GetComponent<CharacterMovement> ().player1;
			}
			thisTimer += Time.deltaTime;
			if (thisTimer >= (5f *Time.deltaTime)) {
				thisTimer = 0.0f;
			}
			if (player1 && Input.GetKeyDown("e")) {
				//return;
				suppressingFire = false;
			}
			if (!player1 && Input.GetKeyDown("[9]")) {
				//return;
				suppressingFire = false;
			}
		}//end of while
	}

	void OnTriggerEnter(Collider other) {

	}

	void OnTriggerExit(Collider other) {

	}
}
