using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineState {
	protected Mine mine;
	public virtual void Update(float dt) { }
	public virtual void Enter() { }
	public virtual void Start(Mine owner) { }

	protected int DetectPlayers(float detectDist) {
		float minMag = Mathf.Infinity;
		int playerIndex = -1;
		for(int i = 0; i < Player.players.Count; i++) {
			var mag = (Player.players[i].transform.position - mine.transform.position).sqrMagnitude;
			if(mag < minMag) {
				minMag = mag;
				playerIndex = i;
			}
		}
		if(minMag <= detectDist)
			return playerIndex;
		return -1;
	}
}