using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine;

[System.Serializable]
public class Level : MonoBehaviour {
	public e.Game game;
	public e.BallHistory ballHistory;

	public int yellowBallsCount;
	public int redBallsCount;
	public int blueBallsCount;
	public int greenBallsCount;
	public int purpleBallsCount;
	private Dictionary<e.Color, int> balls = null;
	public GameObject GeneratorPrefab;
	public GameObject ColorSwitchPrefab;
	public GameObject BallPrefab;
    private bool isPlay;
  void Start () {
	  	balls = new Dictionary<e.Color, int> {
			{e.Color.Red, redBallsCount},
			{e.Color.Green, greenBallsCount},
			{e.Color.Blue, blueBallsCount},
			{e.Color.Purple, purpleBallsCount},
			{e.Color.Yellow, yellowBallsCount}
		};
		ballHistory = e.BallHistory.GetInstance();
		ballHistory.Clear();
		
		var basket = new e.Bascket(e.Color.Green, 3, 7);
		var basket2 = new e.Bascket(e.Color.Green, 1, 7);
		var basket3 = new e.Bascket(e.Color.Green, 2, 7);
		
		this.game = new e.Game();
		game.AddObject(basket);
		game.AddObject(basket2);
		game.AddObject(basket3);
	}

	float time = 0.0F;
	
	void Update () {
	}

	public void Play () {
		var g = new e.BallGenerator(balls, 2, -1);
		
		game.AddObject(g);

		for (var i = 0; i < 350; i++) {
			game.Step();
		}
		
		foreach(var obj in game.GetCurrentWorld()) {
			if (obj is e.BallGenerator) {
				var clone = Instantiate(this.GeneratorPrefab);
				clone.GetComponent<Generator>().BallGenerator = obj as e.BallGenerator;
			}
			else if (obj is e.ColorSwitch) {
				var clone = Instantiate(this.ColorSwitchPrefab);
				clone.GetComponent<ColorSwitch>().ColorSwitcher = obj as e.ColorSwitch;
				clone.GetComponent<ColorSwitch>().active = true;
			}
		}

		for (var i = 0; i < ballHistory.GetList().Count; i++ ) {
			var ball = ballHistory.GetList()[i];
		  	var clone = Instantiate(this.BallPrefab);
			clone.GetComponent<Ball>().eBall = ball;
			e.Color originColor = ball.GetActions()[0].Color;
			clone.GetComponent<SpriteRenderer>().sprite = clone.GetComponent<Ball>().GetSpriteByColor(originColor); 
		}
	}
}
