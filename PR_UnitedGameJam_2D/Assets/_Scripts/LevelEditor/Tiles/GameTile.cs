using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour {

	public LevelLoader level;

	public bool CanActivateTiles = false;
	public bool CanBeActivated = false;

	private List<Vector2Int> targets = new List<Vector2Int>();

	private void OnCollisionEnter2D(Collision2D collision) {
		if (CanActivateTiles && collision.gameObject.CompareTag("Bullet")) {
			ActivateTargets();
			Destroy(collision.gameObject);
		}
	}

	public void ActivateTargets() {
		foreach (Vector2Int target in targets) {
			level.Activate(target);
		}
	}

	public void AttemptActivate() {
		if (CanBeActivated) Activate();
	}

	public virtual void Activate() {

	}

	public void AddConnectionTarget(Vector2Int target) {
		targets.Add(target);
	}

}
