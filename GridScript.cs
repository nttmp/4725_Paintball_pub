using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {

	//Legal moves from this gridsquare
	public bool up = true;
	public bool down = true;
	public bool right = true;
	public bool left = true;
	//Used to center the player upon first entering grid
	public bool entered;
	//Keep a reference to the player gameobject in this grid square
	private GameObject player;
	//Toggle if I should be homing in on grids or not
	public bool mouseNav;
	//A reference to the mouseControl object
	private GameObject mouseController;


	// Use this for initialization
	void Start () {
		entered = false;
		mouseNav = true;
		mouseController = GameObject.FindWithTag ("MouseControl");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (mouseController.GetComponent<MouseControlScript>().mouseNav == false) {//if (!mouseNav) {
			//When the player first enters this grid, move them to the center of the square
			if (entered) {
				moveToCenter();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if (mouseController.GetComponent<MouseControlScript>().mouseNav == false) {//if (!mouseNav) {
				entered = true;
				player = other.gameObject;
				player.GetComponent<CharacterMovement>().moveable = false;
			}
		}
	}

	void OnTriggerExit(Collider other) {

	}

	void moveToCenter() {
		if (player.transform.position == transform.position) {
			entered = false;
			player.GetComponent<CharacterMovement>().controlable = true;
		}
		else {
			player.transform.position = Vector3.MoveTowards (player.transform.position, transform.position, player.GetComponent<CharacterMovement>().speed);
		}
	}
}
