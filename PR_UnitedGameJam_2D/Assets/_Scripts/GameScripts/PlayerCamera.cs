using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	Transform target;

	void Start() {
		
	}

	void Update() {
		if (!target) {
			target = GameObject.FindGameObjectWithTag("Player")?.transform;
			return;
		}

		Vector2 newPos = Vector2.Lerp(transform.position, target.position, Time.deltaTime);
		transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
	}
}
