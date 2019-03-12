using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine;

[System.Serializable]
public class Level : MonoBehaviour {
	public e.Game game;
	public e.BallHistory ballHistory;

	public List<e.Ball> ballList;
	public GameObject GeneratorPrefab;
	public GameObject ColorSwitchPrefab;
	public GameObject BallPrefab;
    private bool isPlay;

    void Start () {
		ballHistory = e.BallHistory.GetInstance();

		var g = new e.BallGenerator(new Dictionary<e.Color, int>
		{
			{e.Color.Green, 1},
			{e.Color.Red, 1}
		}, 2, 0);
		
		var ifBlock = new e.ColorCondition(e.Color.Green, new e.Point(-1, 0),  2, 4);
		var chColor = new e.ColorSwitch(e.Color.Red, e.Color.Green, 3, 8);
		var basket = new e.Bascket(e.Color.Green, 3, 10);
		
		this.game = new e.Game();
		game.AddObject(g);
		game.AddObject(ifBlock);
		game.AddObject(chColor);
		game.AddObject(basket);
	}

	float time = 0.0F;
	
	void Update () {
	}

	public void Play () {
		for (var i = 0; i < 17; i++) {
			game.Step();
		}

		foreach(var obj in game.GetCurrentWorld()) {
			if (obj is e.BallGenerator) {
				var clone = Instantiate(this.GeneratorPrefab);
				clone.GetComponent<Generator>().BallGenerator = obj as e.BallGenerator;
			} else if (obj is e.ColorSwitch) {
				var clone = Instantiate(this.ColorSwitchPrefab);
				clone.GetComponent<ColorSwitch>().ColorSwitcher = obj as e.ColorSwitch;
			}
		}

		Debug.Log("dsf");
		for (var i = 0; i < ballHistory.GetList().Count; i++ ) {
			var ball = ballHistory.GetList()[i];
			var clone = Instantiate(this.BallPrefab);
			// clone.GetComponent<Ball>().eBall = ball;
		}
	}
}
