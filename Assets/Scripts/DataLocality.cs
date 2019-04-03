using System;
using UnityEngine;
// taken from: https://jacksondunstan.com/articles/3860
class DataLocality : MonoBehaviour {
	struct ProjectileStruct {
		public Vector3 Position;
		public Vector3 Velocity;
	}

	class ProjectileClass {
		public Vector3 Position;
		public Vector3 Velocity;
	}

	void Start() {
		const int count = 10000000;
		ProjectileStruct[] projectileStructs = new ProjectileStruct[count];
		ProjectileClass[] projectileClasses = new ProjectileClass[count];
		for(int i = 0; i < count; ++i) {
			//class
			projectileClasses[i] = new ProjectileClass();
			projectileClasses[i].Velocity = UnityEngine.Random.onUnitSphere;
			//struct
			projectileStructs[i].Velocity = UnityEngine.Random.onUnitSphere;
		}
		// Shuffle to simulate objects being destroyed and created anew
		Shuffle(projectileStructs);
		Shuffle(projectileClasses);

		System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
		for(int i = 0; i < count; ++i) {
			UpdateProjectile(ref projectileStructs[i], 0.5f);
		}
		long structTime = sw.ElapsedMilliseconds;

		sw.Reset();
		sw.Start();
		for(int i = 0; i < count; ++i) {
			UpdateProjectile(projectileClasses[i], 0.5f);
		}
		long classTime = sw.ElapsedMilliseconds;

		string report = string.Format("Struct: {0}, Class: {1}", structTime, classTime);
		Debug.Log(report);
	}

	void UpdateProjectile(ref ProjectileStruct projectile, float time) {
		projectile.Position += projectile.Velocity * time;
	}

	void UpdateProjectile(ProjectileClass projectile, float time) {
		projectile.Position += projectile.Velocity * time;
	}

	public static void Shuffle<T>(T[] list) {
		System.Random random = new System.Random();
		for(int n = list.Length; n > 1;) {
			n--;
			int k = random.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}