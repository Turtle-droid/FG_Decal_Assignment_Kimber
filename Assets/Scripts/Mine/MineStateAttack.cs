using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MineStateAttack : MineState {
	public float chasingDistance;
	[System.NonSerialized]
	public Player chasingPlayer;
	public override void Start(Mine owner) {
		mine = owner;
	}
	public override void Enter() {
		mine.currentState = this;
	}
	public override void Update(float dt) {
		if(chasingPlayer.alive) {
			var mag = (chasingPlayer.transform.position - mine.transform.position).sqrMagnitude;
			if(mag <= chasingDistance * chasingDistance) {
				mine.rb.AddForce(chasingPlayer.transform.position - mine.transform.position, ForceMode.Acceleration);
				return;
			}
		}
		mine.idleState.Enter();
		return;
	}
}
