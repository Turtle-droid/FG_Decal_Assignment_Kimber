using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Rigidbody rb;
	float speed = 22f;

	public Player owner;

	private void OnCollisionEnter(Collision collision) {
		var player = collision.collider.GetComponent<Player>();
		if(player != null) {
			player.Kill(owner);
		}
	}

	//
	// No pool
	//
	
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);

		Destroy(gameObject, 3f);
    }
	
	
	

	


	//
	// Pooled 1
	//
	/*
	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	public void Launch() {
		rb.velocity = transform.forward * speed;
		StopAllCoroutines();
		StartCoroutine(ReturnToPoolAfterAWhile());
	}

	IEnumerator ReturnToPoolAfterAWhile() {
		yield return new WaitForSeconds(3f);
		poolOwner.ReturnBullet(poolIndex);
	}

	[System.NonSerialized]
	public Gun poolOwner;
	[System.NonSerialized]
	public int poolIndex;
	*/





	//
	// Pooled 2
	//
	/*
	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	public void Launch() {
		rb.velocity = transform.forward * speed;
		StopAllCoroutines();
		StartCoroutine(DisableAfterAWhile());
	}

	IEnumerator DisableAfterAWhile() {
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}
	
	[System.NonSerialized]
	public Gun poolOwner;
	[System.NonSerialized]
	public int poolIndex;
	*/
}
