using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Grid))]
public class LevelEditor : MonoBehaviour {

	public string selectedTileID = "";
	public TileManager tileManager;
	public TileSpriteManager tileSpriteManager;

	public GameObject levelEditorCell;

	private Grid grid;

	private Dictionary<Vector2Int, LevelEditorCell> levelCells;

	private Vector2 prevMousePosition;
	private bool prevMouseDown = false;
	private bool isDragging = false;

	private ILevelEditorTool tool;

	void Awake() {
		tileSpriteManager.Init();
		tileManager.Init();

		grid = GetComponent<Grid>();
		levelCells = new Dictionary<Vector2Int, LevelEditorCell>();
	}

	void Start() {
		
	}

	void Update() {
		Vector2 nextMousePosition = Input.mousePosition + new Vector3(8f, 8f);
		bool nextMouseDown = Input.GetMouseButton(0);

		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (!prevMouseDown && nextMouseDown) tool?.OnMouseDown(this, ScreenToCell(nextMousePosition), nextMousePosition);

			if (!isDragging && nextMouseDown) {
				tool?.OnMouseStartDrag(this, ScreenToCell(nextMousePosition), nextMousePosition);
				isDragging = true;
			}
		}

		if (prevMouseDown && !nextMouseDown) tool?.OnMouseUp(this, ScreenToCell(nextMousePosition), nextMousePosition);

		if (isDragging && !nextMouseDown) {
			tool?.OnMouseEndDrag(this, ScreenToCell(nextMousePosition), nextMousePosition);
			isDragging = false;
		}

		if (isDragging) tool?.OnMouseDrag(this, ScreenToCell(nextMousePosition), nextMousePosition);
		

		prevMousePosition = nextMousePosition;
		prevMouseDown = nextMouseDown;
	}

	public string GetIDAt(Vector2Int position) {
		return levelCells.ContainsKey(position) ? levelCells[position].GetTileData().GetID() : "nulltile";
	}

	public Tile GetTileDataAt(Vector2Int position) {
		return levelCells.ContainsKey(position) ? levelCells[position].GetTileData() : null;
	}

	public void SetTile(Vector2Int position, Tile tile) {
		if (levelCells.ContainsKey(position)) {
			levelCells[position].SetTile(tile, this);
		} else {
			GameObject newCell = Instantiate(levelEditorCell, grid.CellToLocal((Vector3Int) position), Quaternion.identity, grid.transform);
			LevelEditorCell newLevelCell = newCell.GetComponent<LevelEditorCell>();
			newLevelCell.SetTile(tile, this);
			newLevelCell.levelEditor = this;
			levelCells.Add(position, newLevelCell);
		}

		// Update adjacent
		if (levelCells.ContainsKey(position + Vector2Int.left)) levelCells[position + Vector2Int.left].UpdateTileState(this);
		if (levelCells.ContainsKey(position + Vector2Int.right)) levelCells[position + Vector2Int.right].UpdateTileState(this);
		if (levelCells.ContainsKey(position + Vector2Int.up)) levelCells[position + Vector2Int.up].UpdateTileState(this);
		if (levelCells.ContainsKey(position + Vector2Int.down)) levelCells[position + Vector2Int.down].UpdateTileState(this);
	}

	public void RemoveTile(Vector2Int position) {
		if (levelCells.ContainsKey(position)) {
			Destroy(levelCells[position].gameObject);
			levelCells.Remove(position);
		}

		// Update adjacent
		if (levelCells.ContainsKey(position + Vector2Int.left)) levelCells[position + Vector2Int.left].UpdateTileState(this);
		if (levelCells.ContainsKey(position + Vector2Int.right)) levelCells[position + Vector2Int.right].UpdateTileState(this);
		if (levelCells.ContainsKey(position + Vector2Int.up)) levelCells[position + Vector2Int.up].UpdateTileState(this);
		if (levelCells.ContainsKey(position + Vector2Int.down)) levelCells[position + Vector2Int.down].UpdateTileState(this);
	}

	public Tile GetSelectedTile() {
		return tileManager.GetTile(selectedTileID);
	}

	public void SetTile(string id) {
		selectedTileID = id;
	}

	public bool CellExists(Vector2Int position) {
		return levelCells.ContainsKey(position);
	}

	public Vector2Int ScreenToCell(Vector2 mousePosition) {
		return (Vector2Int) grid.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));
	}

	public Vector2Int WorldToCell(Vector2 worldPosition) {
		return (Vector2Int) grid.WorldToCell(worldPosition);
	}

	public Vector2 CellToWorld(Vector2Int cellPosition) {
		return grid.CellToWorld((Vector3Int)cellPosition);
	}

	public void SetTool(ILevelEditorTool tool) {
		this.tool = tool;
	}

	public void AddWire(Vector2Int activator, Vector2Int target) {
		levelCells[activator].AddWire(target);
	}

	//public Bounds GetExtents() {
	//
	//}

	public void Export() {
		SaveToFile("C:\\Users\\sepul\\Desktop", "level");
	}

	public void SaveToFile(string path, string name) {
		XmlDocument newLevel = new XmlDocument();
		XmlElement root = newLevel.CreateElement("LEVEL");
		root.SetAttribute("id", name);
		newLevel.AppendChild(root);

		foreach (Vector2Int position in levelCells.Keys) {
			Tile tile = levelCells[position].GetTileData();
			XmlElement tileXML = newLevel.CreateElement("TILE");
			tileXML.SetAttribute("type", $"{tile.GetID()}:{tile.GetSpriteID(levelCells[position].GetSpriteIndex())}");
			tileXML.SetAttribute("x", $"{position.x}");
			tileXML.SetAttribute("y", $"{position.y}");

			LevelEditorCell.Wire[] connections = levelCells[position].GetConnections();

			if (connections.Length > 0) {
				XmlElement tileWireXML = newLevel.CreateElement("CONNECTIONS");
				tileXML.AppendChild(tileWireXML);


				foreach (LevelEditorCell.Wire wire in connections) {
					XmlElement wireXML = newLevel.CreateElement("WIRE");
					wireXML.SetAttribute("targetX", $"{wire.target.x}");
					wireXML.SetAttribute("targetY", $"{wire.target.y}");

					tileWireXML.AppendChild(wireXML);
				}
			}

			root.AppendChild(tileXML);
		}

		newLevel.Save(path + "\\" + name + ".xml");
	}

}
