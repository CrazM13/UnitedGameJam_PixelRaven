using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureGameTile : GameTile {

	public LoadStorageObject levelQueue;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			if (levelQueue.HasNext()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			else SceneManager.LoadScene(0);
		}
	}

}
