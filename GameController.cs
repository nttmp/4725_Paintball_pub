using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//Player 1's objects
	public GameObject p1Goal;
	public GameObject p1p1;
	public GameObject p1p2;
	public GameObject p1p3;
	public GameObject p1p4;
	//Player 2's objects
	public GameObject p2Goal;
	public GameObject p2p1;
	public GameObject p2p2;
	public GameObject p2p3;
	public GameObject p2p4;
	//The fucking lights
	public Light p1p1L;
	public Light p1p2L;
	public Light p1p3L;
	public Light p1p4L;
	public Light p2p1L;
	public Light p2p2L;
	public Light p2p3L;
	public Light p2p4L;
	//References to the currently controllable objects
	public GameObject p1Curr; //Changed to public for the mouse script
	private GameObject p2Curr;
	//Gui stuff
	public GUIText winStuff;
	//Control the hotkey to restart the game and stuff
	public bool gameOver = false;
	//Get the control initialization to happen a litle bit later
	private bool initBuffer;
	//Arrays to store the list of characters, per player
	// List<GameObject> p1List = new List<GameObject>();

	// Use this for initialization
	void Start () {
		winStuff.enabled = false;
		initBuffer = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (initBuffer) {
			initActivated ();
		}
		handleSwitching ();
		winScreen (checkForDeath ());
		if(gameOver) {
			if (Input.GetKeyDown("r")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	
	}

	public void initActivated() {
		for (int i = 0; i < 50; i++) {
			//Debug.Log ("Buffer time");		
		}
		p1p1.GetComponent<CharacterMovement> ().activated = true;
		p1p2.GetComponent<CharacterMovement> ().activated = false;
		p1p3.GetComponent<CharacterMovement> ().activated = false;
		p1p4.GetComponent<CharacterMovement> ().activated = false;
		p1Curr = p1p1;

		p2p1.GetComponent<CharacterMovement> ().activated = true;
		p2p2.GetComponent<CharacterMovement> ().activated = false;
		p2p3.GetComponent<CharacterMovement> ().activated = false;
		p2p3.GetComponent<CharacterMovement> ().activated = false;
		p2Curr = p2p1;

		turnOffLights ();
		p1p1L.enabled = true;
		p2p1L.enabled = true;
		initBuffer = false;
	}

	private void handleSwitching() {
		//Player 1
		if (Input.GetKeyDown("1") && p1p1.activeInHierarchy) {
			p1Curr.GetComponent<CharacterMovement> ().activated = false;
			p1p1.GetComponent<CharacterMovement> ().activated = true;
			p1Curr = p1p1;
			turnOffLightsP1();
			p1p1L.enabled = true;
		}
		if (Input.GetKeyDown("2") && p1p2.activeInHierarchy) {
			p1Curr.GetComponent<CharacterMovement> ().activated = false;
			p1p2.GetComponent<CharacterMovement> ().activated = true;
			p1Curr = p1p2;
			turnOffLightsP1();
			p1p2L.enabled = true;
		}
		if (Input.GetKeyDown("3") && p1p3.activeInHierarchy) {
			p1Curr.GetComponent<CharacterMovement> ().activated = false;
			p1p3.GetComponent<CharacterMovement> ().activated = true;
			p1Curr = p1p3;
			turnOffLightsP1();
			p1p3L.enabled = true;
		}
		if (Input.GetKeyDown("4") && p1p4.activeInHierarchy) {
			p1Curr.GetComponent<CharacterMovement> ().activated = false;
			p1p4.GetComponent<CharacterMovement> ().activated = true;
			p1Curr = p1p4;
			turnOffLightsP1();
			p1p4L.enabled = true;
		}
		//Player 2
		if (Input.GetKeyDown("[1]") && p2p1.activeInHierarchy) {
			p2Curr.GetComponent<CharacterMovement> ().activated = false;
			p2p1.GetComponent<CharacterMovement> ().activated = true;
			p2Curr = p2p1;
			turnOffLightsP2();
			p2p1L.enabled = true;
		}
		if (Input.GetKeyDown("[2]") && p2p2.activeInHierarchy) {
			p2Curr.GetComponent<CharacterMovement> ().activated = false;
			p2p2.GetComponent<CharacterMovement> ().activated = true;
			p2Curr = p2p2;
			turnOffLightsP2();
			p2p2L.enabled = true;
		}
		if (Input.GetKeyDown("[3]") && p2p3.activeInHierarchy) {
			p2Curr.GetComponent<CharacterMovement> ().activated = false;
			p2p3.GetComponent<CharacterMovement> ().activated = true;
			p2Curr = p2p3;
			turnOffLightsP2();
			p2p3L.enabled = true;
		}
		if (Input.GetKeyDown("[0]") && p2p4.activeInHierarchy) {
			p2Curr.GetComponent<CharacterMovement> ().activated = false;
			p2p4.GetComponent<CharacterMovement> ().activated = true;
			p2Curr = p2p4;
			turnOffLightsP2();
			p2p4L.enabled = true;
		}
		
	}

	public int checkForDeath() {

		if (p1p1.activeInHierarchy == false && p1p2.activeInHierarchy == false &&
		    p1p3.activeInHierarchy == false && p1p4.activeInHierarchy == false) {
			return 2;
		}
		else if  (p2p1.activeInHierarchy == false && p2p2.activeInHierarchy == false &&
		          p2p3.activeInHierarchy == false && p2p4.activeInHierarchy == false) {
			return 1;
		}
		else {
			return 0;
		}
	}

	public void winScreen(int winner) {
		if (winner == 1) {
			winStuff.text = "PLAYER 1 WINS";
			gameOver = true;
		}
		if (winner == 2) {
			winStuff.text = "PLAYER 2 WINS";
			gameOver = true;
		}
		if (gameOver) {
			winStuff.enabled = true;
		}

	}

	void turnOffLights() {
		p1p1L.enabled = false;
		p1p2L.enabled = false;
		p1p3L.enabled = false;
		p1p4L.enabled = false;
		p2p1L.enabled = false;
		p2p2L.enabled = false;
		p2p3L.enabled = false;
		p2p4L.enabled = false;
	}

	void turnOffLightsP1() {
		p1p1L.enabled = false;
		p1p2L.enabled = false;
		p1p3L.enabled = false;
		p1p4L.enabled = false;
	}

	void turnOffLightsP2() {
		p2p1L.enabled = false;
		p2p2L.enabled = false;
		p2p3L.enabled = false;
		p2p4L.enabled = false;
	}


}
