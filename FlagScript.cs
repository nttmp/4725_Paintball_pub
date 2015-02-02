using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour {

	public GameObject gameController;
	//Is this player 1's goal or not?
	public bool player1;
	private int thisPlayer;// = 0;

	// Use this for initialization
	void Start () {
		thisPlayer = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player"){
			if (player1 == other.gameObject.GetComponent<CharacterMovement>().player1) {
				if (player1) {
					gameController.GetComponent<GameController>().winScreen(1);
				}
				else {
					gameController.GetComponent<GameController>().winScreen(2);
				}
			}
		}
	}
}
