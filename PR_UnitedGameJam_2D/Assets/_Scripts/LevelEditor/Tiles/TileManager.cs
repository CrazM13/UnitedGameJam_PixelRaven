using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileManager", menuName = "LevelEditor/Tile Manager", order = 0)]
public class TileManager : ScriptableObject {

	public TextAsset[] tileImportFiles;

	private Dictionary<string, Tile> tiles = new Dictionary<string, Tile>();

	public void Init() {
		foreach (TextAsset tileImport in tileImportFiles) {
			Tile tile = new Tile(tileImport.text);
			tiles.Add(tile.GetID(), tile);
		}
	}

	public Tile GetTile(string id) {
		return tiles[id];
	}

}
