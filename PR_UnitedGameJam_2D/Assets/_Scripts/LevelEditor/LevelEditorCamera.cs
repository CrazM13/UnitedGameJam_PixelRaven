using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditorCamera : MonoBehaviour {

	public float speed;
	public Vector2 margins;

	void Start() {

	}

	void Update() {
		if (!EventSystem.current.IsPointerOverGameObject()) {
			Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			if (mousePos.x < 0 || mousePos.x > 1 || mousePos.y < 0 || mousePos.y > 1) return;

			if (mousePos.x < margins.x) transform.Translate(Vector3.left * speed, Space.World);
			if (mousePos.x > 1 - margins.x) transform.Translate(Vector3.right * speed, Space.World);
			if (mousePos.y > 1 - margins.x) transform.Translate(Vector3.up * speed, Space.World);
			if (mousePos.y < margins.x) transform.Translate(Vector3.down * speed, Space.World);

		}
	}
}
