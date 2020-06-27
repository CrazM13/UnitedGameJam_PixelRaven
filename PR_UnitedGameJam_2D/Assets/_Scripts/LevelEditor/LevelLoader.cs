using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public GameTileManager gameTileManager;
	public TileSpriteManager spriteManager;

	public TextAsset level;

	private Dictionary<Vector2Int, GameTile> tiles = new Dictionary<Vector2Int, GameTile>();

	private Grid grid;

	private void Awake() {
		spriteManager.Init();

		grid = GetComponent<Grid>();
	}

	void Start() {
		Load();
	}

	void Update() {

	}

	public void Load() {
		XmlDocument importData = new XmlDocument();
		importData.LoadXml(level.text);

		XmlElement root = importData["LEVEL"];
		
		foreach(XmlElement tile in root.ChildNodes) {
			string id = tile.GetAttribute("type");

			GameObject newTile = gameTileManager.GetPrefabByID(id);
			if (!newTile) newTile = gameTileManager.GenerateTileByID(id);

			newTile.transform.SetParent(transform);
			
			Vector2Int position = new Vector2Int(int.Parse(tile.GetAttribute("x")), int.Parse(tile.GetAttribute("y")));

			newTile.transform.position = grid.CellToWorld((Vector3Int)position);

			if (tile["CONNECTIONS"] != null) {
				foreach (XmlElement connection in tile["CONNECTIONS"].ChildNodes) {
					newTile.GetComponent<GameTile>().AddConnectionTarget(new Vector2Int(int.Parse(connection.GetAttribute("targetX")), int.Parse(connection.GetAttribute("targetY"))));
				}
			}

			tiles.Add(position, newTile.GetComponent<GameTile>());

		}

	}

	public void Activate(Vector2Int target) {
		tiles[target].AttemptActivate();
	}

}
