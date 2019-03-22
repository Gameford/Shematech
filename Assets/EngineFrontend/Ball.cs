﻿using System.Collections;
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

			var actions = this.eBall.GetActions();
			if (actions.Count > step) {
				var action = actions[step];

				this.transform.position = GameObject.Find("Grid").GetComponent<GridInit>().cellsArray[action.Position.Y, action.Position.X].transform.position;
				switch (action.Type) {
				}
			}
			

			step++;
		}
	}
}
