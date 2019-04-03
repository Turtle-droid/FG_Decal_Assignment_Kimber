using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MineStateIdle : MineState {
	public float detectDist;
	Vector3 homePos;
	public override void Start(Mine owner) {
		mine = owner;
		homePos = mine.transform.position;
	}
	public override void Enter() {
		mine.currentState = this;
	}
	public override void Update(float dt) {
		if(DetectPlayers(detectDist) != -1) {
			mine.detectState.Enter();
			return;
		}

		mine.rb.AddForce(homePos - mine.transform.position, ForceMode.Acceleration);
	}
}