using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine;

public class ColorSwitch : MonoBehaviour {
	public e.ColorSwitch ColorSwitcher;

	public float time = 0;
	public int step = 0;

	private bool active = false;

	void Start () {

	}
	
	void Update () {
		if (!this.active) { return; }

		time += Time.deltaTime;

		if (time >= 1) {
			time = 0;

			var actions = this.ColorSwitcher.GetActions()[step];
			this.transform.position = new Vector2(actions.Position.X, actions.Position.Y);
			switch (actions.Type) {
			}

			step++;
		}
	}
}
