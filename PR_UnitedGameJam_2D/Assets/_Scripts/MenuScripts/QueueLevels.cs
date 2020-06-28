using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueLevels : MonoBehaviour {

	public LoadStorageObject levelQueue;
	public TextAsset[] levels;

	public void Queue() {
		foreach (TextAsset lvl in levels) levelQueue.QueueLevel(lvl);
	}
}
