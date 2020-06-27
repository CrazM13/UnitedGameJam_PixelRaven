using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorTileMenu : MonoBehaviour {

	public TileManager tileManager;
	public LevelEditor levelEditor;

	public GameObject prefab;
	public Transform container;

	void Start() {
		foreach(Tile t in tileManager.GetTiles()) {
			GameObject newBtn = Instantiate(prefab, container);
			newBtn.GetComponent<LevelEditorTileSelect>().tileID = t.GetID();
			newBtn.GetComponent<LevelEditorTileSelect>().levelEditor = levelEditor;
			newBtn.GetComponent<Image>().sprite = t.GetSprite(0);
		}
	}
}
