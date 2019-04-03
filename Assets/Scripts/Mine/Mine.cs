using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
	public MineStateDetect detectState;
	public MineStateIdle idleState;
	public MineStateAttack attackState;
	[System.NonSerialized]
	public MineState currentState;
	[System.NonSerialized]
	public Rigidbody rb;
	
    void Start()
    {
		rb = GetComponent<Rigidbody>();

		detectState.Start(this);
		idleState.Start(this);
		attackState.Start(this);
		
		idleState.Enter();
    }
	
    void Update()
    {
		currentState.Update(Time.deltaTime);
    }

	private void OnCollisionEnter(Collision collision) {
		var player = collision.collider.GetComponent<Player>();
		if(player != null) {
			player.Kill(null);
			Destroy(gameObject);
		}
	}
}
