using UnityEngine;
using System.Collections;

public class MouseControlRaycast : MonoBehaviour {
	
	private GameObject currentGrid;
	public Vector3 targetPos;
	public Vector3 currGridPos; //Just used for observation in the inspector
	public bool mouseNav;
	//A reference to the currently moveable object
	private GameObject currObj;
	//A reference to the gameControllerScript
	public GameObject gameController;
	//A reference to bullet prefab
	public Rigidbody bullet;
	//A variable to control continuous fire
	public bool isShooting;

	//Raycasting Stuff
	//Store the distance between the current player to be moved and the fingermover
	public float RayCastDist;
	//RaycastHit
	private RaycastHit hit;


	
	
	// Use this for initialization
	void Start () {
		mouseNav = false;
		gameController = GameObject.FindWithTag ("GameController");
		isShooting = false;
	}
	
	// Update is called once per frame
	void Update () {
		//currentGrid.GetComponent<GridScript>().mouseNav = mouseNav;
		if (Input.GetMouseButtonDown(0)) {
			targetPos = currentGrid.transform.position;
			currObj = gameController.GetComponent<GameController>().p1Curr;
			mouseNav = true;
			RayCastDist = Vector3.Distance(currObj.transform.position, targetPos);
		}
		if (mouseNav) {
			moveTowardsGrid();
		}
	}
	
	void OnTriggerEnter(Collider other) {
		//Remember, this script goes to the FingerMover object, so the following
		//stores the current grid and it's location in world space
		if (other.tag == "Grid") {
			currentGrid = other.gameObject;
			currGridPos = currentGrid.transform.position;
		}
	}
	
	void moveTowardsGrid() {
		if (currObj.transform.position == targetPos) {
			mouseNav = false;
		}
		else {
			currObj.transform.position = Vector3.MoveTowards (currObj.transform.position, targetPos, currObj.GetComponent<CharacterMovement>().speed);
		}
	}
}
