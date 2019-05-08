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
				var pos = action.Position;
				if (pos.X >= 0 && pos.Y >= 0) {
					cell = grid.cellsArray[pos.Y, pos.X];
					var cell_actions = this.eBall.GetActionsByCell(pos.X, pos.Y);
					foreach(var cell_action in cell_actions) {
						switch (cell_action.Type) {
							case e.ActionType.BallProduced:
								
							break;
							case e.ActionType.BallMove:
								if (cell) {
									this.transform.position = cell.transform.position;
								}
								else if(false) {

								}
							break;
							case e.ActionType.BallChangeColor:
								Sprite[] s = Resources.LoadAll<Sprite>("Graphics/GameField/robots");
								this.GetComponent<SpriteRenderer>().sprite = s[0];
							break;
						}
					}
				}
			}
			

			step++;
		}
	}
}
