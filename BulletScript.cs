using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public int shootDir = 0; //0 if not moving
	public float speed;
	//detect which player this was shot from
	public bool player1;
	//Game logic controller
	public GameObject gameController;
	//For the mouse targeting junk
	public Vector3 mouseTarget;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		//Up
		if (shootDir == 1) {
			transform.Translate(Vector3.forward * speed);
		}
		//Down
		if (shootDir == 2) {
			transform.Translate(Vector3.back * speed);
		}
		//Left
		if (shootDir == 3) {
			transform.Translate(Vector3.left * speed);
		}
		//Right
		if (shootDir == 4) {
			transform.Translate(Vector3.right * speed);
		}
		//Mouse target
		if (shootDir == 5) {
			transform.Translate(mouseTarget * speed);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if (player1 != other.gameObject.GetComponent<CharacterMovement>().player1) {
				if (other.gameObject.GetComponent<CharacterMovement>().dodge == false) {
					int playerToCheck = gameController.GetComponent<GameController>().checkForDeath();
					//Destroy(other.gameObject);
					other.gameObject.SetActive(false);
					gameController.GetComponent<GameController>().winScreen(playerToCheck);
					Destroy (this.gameObject);
				}
			}
		}
	}
}
