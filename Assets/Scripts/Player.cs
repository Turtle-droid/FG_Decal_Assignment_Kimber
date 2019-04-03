using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public KeyCode forwardKey;
	public KeyCode backKey;
	public KeyCode turnLeftKey;
	public KeyCode turnRightKey;

	public KeyCode shootKey;

	const float MOVESPEED = 8f;
	const float TURNSPEED = 20f;

	Gun gun;
	Rigidbody rb;
	Renderer[] renderers;

	float killTime;


	public static List<Player> players = new List<Player>();
	private void OnEnable() {
		players.Add(this);
	}
	private void OnDisable() {
		players.Remove(this);
	}

	[System.NonSerialized]
	public int frags;
	[System.NonSerialized]
	public bool alive = true;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		renderers = GetComponentsInChildren<Renderer>();
		gun = GetComponentInChildren<Gun>();
		gun.owner = this;
    }
	

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKey(shootKey)) {
			if (killTime <= Time.time) {
				if(alive)
					gun.Shoot();
				else
					Respawn();
			}
		}

		if(!alive)
			return;

		if(Input.GetKey(forwardKey)) {
			rb.AddRelativeForce(Vector3.forward * MOVESPEED, ForceMode.Acceleration);
		}
		else if(Input.GetKey(backKey)) {
			rb.AddRelativeForce(Vector3.back * MOVESPEED, ForceMode.Acceleration);
		}

		if(Input.GetKey(turnLeftKey)) {
			rb.AddRelativeTorque(Vector3.down * TURNSPEED, ForceMode.Acceleration);
		}
		else if(Input.GetKey(turnRightKey)) {
			rb.AddRelativeTorque(Vector3.up * TURNSPEED, ForceMode.Acceleration);
		}
	}

	public void Kill(Player attacker) {
		killTime = Time.time + 1f;
		rb.isKinematic = true;
		rb.detectCollisions = false;
		foreach(var r in renderers) {
			r.enabled = false;
		}
		alive = false;
		if (attacker)
			attacker.GotKill();
	}

	void Respawn() {
		rb.isKinematic = false;
		rb.detectCollisions = true;
		foreach(var r in renderers) {
			r.enabled = true;
		}
		alive = true;
		killTime = Time.time + 0.5f;

		var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		var selectedPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
		transform.position = selectedPoint.transform.position;
		transform.rotation = selectedPoint.transform.rotation;
	}

	//
	// GameHandler stuff
	//
	void GotKill() {
		frags++;
		GameHandler.GetInstance().TryUpdateFragLeader(this);
	}
}
