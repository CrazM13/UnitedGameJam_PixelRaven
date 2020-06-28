using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ElevatorGameTile : GameTile {

	private static readonly int MAX_HEIGHT = 10;

	private Vector2[] positions;
	private int index = 0;
	private float timer = 0;
	private bool active;
	private float distance = 1;
	public float speed = 2f;

	private void Start() {
		positions = new Vector2[2];
		positions[0] = transform.position;
		positions[1] = GetTargetTopPosition();
	}

	private void Update() {
		if (active) {
			timer += (Time.deltaTime / distance) * speed;
			transform.position = Vector2.Lerp(positions[index], positions[(index + 1) % positions.Length], timer);
			if (timer >= 1) {
				timer -= 1;
				index = (index + 1) % positions.Length;
			}
		}
	}

	public override void Activate() {
		active = !active;
	}

	private Vector2 GetTargetTopPosition() {
		Vector2Int position = level.WorldToCell(transform.position);

		for (int i = 1; i <= MAX_HEIGHT; i++) {
			if (level.IsTileAt(position + new Vector2Int(0, i))) {
				distance = i;
				return level.CellToWorld(position + new Vector2Int(0, i - 1));
			}
		}

		distance = MAX_HEIGHT;
		return position + new Vector2Int(0, MAX_HEIGHT);
	}

}
