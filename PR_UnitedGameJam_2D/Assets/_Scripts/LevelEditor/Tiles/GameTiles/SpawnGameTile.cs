using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameTile : GameTile {

	public GameObject prefab;

	void Start() {
		Instantiate(prefab);
	}
}
