using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loading Storage", menuName = "LevelEditor/Loading Storage Object", order = 0)]
public class LoadStorageObject : ScriptableObject {

	private Queue<TextAsset> levelsQueue = new Queue<TextAsset>();

	public TextAsset GetLevel() {
		return levelsQueue.Dequeue();
	}

	public void QueueLevel(TextAsset level) {
		levelsQueue.Enqueue(level);
	}

	public bool HasNext() {
		return levelsQueue.Count > 0;
	}

}
