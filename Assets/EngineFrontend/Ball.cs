using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine;

public class Ball : MonoBehaviour {
	public e.Ball eBall = null;

	public float time = 0;
	public int step = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time >= 1) {
			time = 0;

			var actions = this.eBall.GetMoveActions();
			if (actions.Count > step) {
				var action = actions[step];
				var grid = GameObject.Find("Grid").GetComponent<GridInit>();
				Cell cell = null;
				switch (action.Type) {
					case e.ActionType.BallProduced:
						
					break;
					case e.ActionType.BallMove:
						cell = grid.cellsArray[action.Position.Y, action.Position.X];
						if (cell) {
							this.transform.position = cell.transform.position;
						}
						else if(CheckConsume(step)) {

						}
					break;
				}
			}
			

			step++;
		}
	}

	bool CheckConsume(int step) {
		var action = this.eBall.GetActions()[step];
		if (this.eBall.GetActions()[step+1].Type == e.ActionType.BallInteract
			&& this.eBall.GetActions()[step+2].Type == e.ActionType.BallConsumed) {
				return true;
		}
		return false;
	}
}
