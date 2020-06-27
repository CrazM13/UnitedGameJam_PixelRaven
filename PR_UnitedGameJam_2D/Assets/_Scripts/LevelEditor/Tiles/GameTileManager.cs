using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameTileManager", menuName = "LevelEditor/Game Tile Manager", order = 0)]
public class GameTileManager : ScriptableObject {

	public GameObject baseTilePrefab;
	public TileIdMap[] tileMaps;

	public GameObject GetPrefabByID(string id) {
		foreach(TileIdMap tileMap in tileMaps) {
			if (tileMap.id == id) return Instantiate(tileMap.prefab);
		}

		return null;
	}

	public GameObject GenerateTileByID(string id, bool canActivateTiles = false, bool canBeActivated = false) {
		Sprite sprite = TileSpriteManager.Load(id.Split(':')[1]);

		GameObject newObj = Instantiate(baseTilePrefab);
		newObj.GetComponent<SpriteRenderer>().sprite = sprite;
		GameTile tile = newObj.GetComponent<GameTile>();

		tile.CanActivateTiles = canActivateTiles;
		tile.CanBeActivated = canBeActivated;

		return newObj;
	}

	[System.Serializable]
	public class TileIdMap {
		public string id;
		public GameObject prefab;
	}

}