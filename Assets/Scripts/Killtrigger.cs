using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killtrigger : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision) {
		var player = collision.collider.GetComponent<Player>();
		if(player != null) {
			player.Kill(null);
		}
	}
}
