using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mine_v2 : MonoBehaviour {
	/*
	// Idle
	Vector3 homePos;
	[Header("IdleState")]
	public float idleSenseDist;

	void EnterIdle() {
		think = IdleThink;
	}

	void IdleThink(float dt) {
		if(DetectPlayers(idleSenseDist) != -1) {
			EnterDetect();
			return;
		}
		rb.AddForce(homePos - transform.position, ForceMode.Acceleration);
	}

	// Detect
	float detectTime;
	[Header("DetectState")]
	public Renderer blinkRenderer;
	public Color blinkOnColor;
	public Color blinkOffColor;
	public AnimationCurve blinkCurve;
	public float detectSenseDist;

	void EnterDetect() {
		think = DetectThink;
		detectTime = Time.time;
	}

	void DetectThink(float dt) {
		var time = Time.time - detectTime;
		var blinkyness = blinkCurve.Evaluate(time);
		blinkRenderer.material.SetColor("_EmissionColor", Color.Lerp(blinkOffColor, blinkOnColor, blinkyness));
		var animLength = blinkCurve[blinkCurve.length - 1].time;
		if(animLength <= time) { // animation is done
			var detectedPlayer = DetectPlayers(detectSenseDist);
			if(detectedPlayer != -1) {
				chasingPlayer = Player.players[detectedPlayer];
				EnterAttack();
				return;
			}
			EnterIdle();
			return;
		}
	}

	// Attack
	Player chasingPlayer;
	[Header("AttackState")]
	public float chasingDistance;

	void EnterAttack() {
		think = AttackThink;
	}

	void AttackThink(float dt) {
		if(chasingPlayer.alive) {
			var mag = (chasingPlayer.transform.position - transform.position).sqrMagnitude;
			if(mag <= chasingDistance * chasingDistance) {
				rb.AddForce(chasingPlayer.transform.position - transform.position, ForceMode.Acceleration);
				return;
			}
		}
		EnterIdle();
		return;
	}
	*/
}