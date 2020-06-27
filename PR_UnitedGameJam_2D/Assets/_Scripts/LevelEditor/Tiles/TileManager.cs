using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		return tiles.ContainsKey(id) ? tiles[id] : null;
	}

	public Tile[] GetTiles() {
		return tiles.Values.ToArray();
	}

}
