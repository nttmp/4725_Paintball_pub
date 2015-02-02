using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For the generic list

public class MouseControlScript : MonoBehaviour {
	
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
	public float rayCastDist;
	//RaycastHit
	private RaycastHit hit;
	private RaycastHit hit2;
	//For layermasking: check for objects only in layer 8, the cover objects layer
	private int coverLayerMask = 1 << 8;
	//Layermask for the normal gridSquares
	private int gridLayerMask = 1 << 9;
	//Layermask for the boundary gridsquares

	//List stuff, in order to allow concurrent moving objects
	//The list of objects to currently be moved
	private List<GameObject> movingObjects = new List<GameObject>();
	//The list of destinations for the objects in movingObjects list
	private List<Vector3> movingObjectTargets = new List<Vector3>();

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
			currObj = gameController.GetComponent<GameController>().p1Curr;
			targetPos = currGridPos; //which is currentGrid.transform.position
			rayCastDist = Vector3.Distance(currObj.transform.position, targetPos);
			//If there is a cover object between the player and destination, set target to closest grid before cover object
//			Debug.DrawLine(currObj.transform.position, targetPos, Color.cyan, 10.0f);
			if (Physics.Raycast(currObj.transform.position, targetPos, out hit, rayCastDist, coverLayerMask)) {
				//Debug.Log ("Succesful Raycast");
				//Debug.Log ("Currently moused over: " + hit.collider.gameObject.name + ", on layer: " + hit.collider.gameObject.layer);
				if (hit.collider.tag == "Cover") {
					Debug.Log ("Hit a cover object");
					Debug.DrawLine(hit.transform.position, currObj.transform.position, Color.yellow, 30.0f);
					if (Physics.Raycast(hit.transform.position, currObj.transform.position, out hit2, rayCastDist, gridLayerMask)) {
						if (hit2.collider.tag == "Grid")
							Debug.Log ("Hit a gridsquare");
						targetPos = hit2.collider.gameObject.transform.position;
					}
				}
			}
			mouseNav = true;

			//List stuff
			if (!movingObjects.Contains(currObj)) {
				Debug.Log ("Adding a new object");
				movingObjects.Add(currObj);
				movingObjectTargets.Add (targetPos);
			}
		}
		if (mouseNav) {
			moveTowardsGrid();
		}
//		if (movingObjects.Count != 0) {
//			listMover();
//		}
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

	//Method for moving multiple gameObjects in a list
	void listMover() {
		float movementSpeed = currObj.GetComponent<CharacterMovement> ().speed;
		Vector3 playerObjectPosition;
		Vector3 playerObjectDestination;
		for(int i = 0; i < movingObjects.Count; i++) {
			playerObjectPosition = movingObjects[i].transform.position;
			playerObjectDestination = movingObjectTargets[i];
			if (playerObjectPosition == playerObjectDestination) {
				Debug.Log ("This object has reached it's destination");
				movingObjects.RemoveAt(i);
				movingObjectTargets.RemoveAt(i);
			}
			else {
				Debug.Log ("Curr: " + playerObjectPosition + " Dest: " + playerObjectDestination);
				playerObjectPosition = Vector3.MoveTowards (playerObjectPosition, playerObjectDestination, movementSpeed);
			}
		}
	}
}
