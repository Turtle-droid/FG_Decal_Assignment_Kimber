using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	float timer = 0;
	const float TIMELIMIT = 600f; // 10 minutes
	const int FRAGLIMIT = 10;

	Player fragLeader = null;

	void Update() {
		timer += Time.deltaTime;
		if(timer >= TIMELIMIT) {
			EndGame();
		}
	}
	public void TryUpdateFragLeader(Player instigator) {
		if(!instigator)
			return;
		if(fragLeader == null || instigator.frags > fragLeader.frags)
			fragLeader = instigator;
		if(fragLeader.frags >= FRAGLIMIT)
			EndGame();
	}
	void EndGame() {
		Debug.LogFormat("{0} wins with {1} frags!", fragLeader.name, fragLeader.frags);
	}

	//
	// "Good"
	//
	
	static GameHandler instance;
	public static GameHandler GetInstance() {
		return instance;
	}

	// called when scene starts
	void Awake() {
		instance = this;
	}
	// called when exiting the scene
	// important to call to release resources that might be in use by the handler
	void OnDestroy() {
		instance = null;
	}
	




	//
	// Lazy Init 1
	//
	/*
	static GameHandler instance;
	public static GameHandler GetInstance() {
		if (instance == null)
			instance = new GameObject("GameHandler").AddComponent<GameHandler>();
		return instance;
	}

	// called when exiting the scene
	// important to call to release resources that might be in use by the handler
	void OnDestroy() {
		instance = null;
	}
	*/

	//
	// Lazy Init 2 (C# static constructor variant) (probably requires DontDestroyOnLoad)
	//
	/*
	static GameHandler instance;
	public static GameHandler GetInstance() {
		return instance;
	}
	static GameHandler() { // creates singleton when attempting to access
		instance = new GameObject("GameHandler").AddComponent<GameHandler>();
		//DontDestroyOnLoad(instance.gameObject);
	}
	*/
}
