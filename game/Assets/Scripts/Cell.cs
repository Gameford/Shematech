using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
	public bool isCollision = false;
	public bool isRightCell = false;
	void OnTriggerStay2D(Collider2D collider) {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		isCollision = true;
	}

	void OnTriggerExit2D(Collider2D collider) {
		isCollision = false;
	}
}
