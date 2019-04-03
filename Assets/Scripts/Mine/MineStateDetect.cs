using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MineStateDetect : MineState {
	public Renderer blinkRenderer;
	public Color onColor;
	public Color offColor;
	public AnimationCurve blinkCurve;
	public float detectDist;

	float detectTime;

	public override void Start(Mine owner) {
		mine = owner;
	}
	public override void Enter() {
		mine.currentState = this;
		detectTime = Time.time;
	}
	public override void Update(float dt) {
		var time = Time.time - detectTime;
		var blinkyness = blinkCurve.Evaluate(time);
		blinkRenderer.material.SetColor("_EmissionColor", Color.Lerp(offColor, onColor, blinkyness));
		var animLength = blinkCurve[blinkCurve.length - 1].time;
		if(animLength <= time) { // animation is done
			var detectedPlayer = DetectPlayers(detectDist);
			if(detectedPlayer != -1) {
				mine.attackState.chasingPlayer = Player.players[detectedPlayer];
				mine.attackState.Enter();
				return;
			}
			mine.idleState.Enter();
			return;
		}
	}
}