using System.Collections;
using System.Collections.Generic;
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
		if (!EventSystem.current.IsPointerOverGameObject()) {
			Vector2 nextMousePosition = Input.mousePosition + new Vector3(8f, 8f);
			bool nextMouseDown = Input.GetMouseButton(0);

			if (!prevMouseDown && nextMouseDown) tool?.OnMouseDown(this, ScreenToCell(nextMousePosition), nextMousePosition);
			if (prevMouseDown && !nextMouseDown) tool?.OnMouseUp(this, ScreenToCell(nextMousePosition), nextMousePosition);

			if (nextMouseDown) {
				if (!isDragging && (prevMousePosition != nextMousePosition)) {
					tool?.OnMouseStartDrag(this, ScreenToCell(nextMousePosition), nextMousePosition);
					isDragging = true;
				}

				if (isDragging && !nextMouseDown) {
					tool?.OnMouseEndDrag(this, ScreenToCell(nextMousePosition), nextMousePosition);
					isDragging = false;
				}

				if (isDragging) tool?.OnMouseDrag(this, ScreenToCell(nextMousePosition), nextMousePosition);
			} else {
				isDragging = false;
			}

			prevMousePosition = nextMousePosition;
			prevMouseDown = nextMouseDown;
		}
	}

	public string GetIDAt(Vector2Int position) {
		return levelCells.ContainsKey(position) ? levelCells[position].GetTileData().GetID() : "nulltile";
	}

	public void SetTile(Vector2Int position, Tile tile) {
		if (levelCells.ContainsKey(position)) {
			levelCells[position].SetTile(tile, this);
		} else {
			GameObject newCell = Instantiate(levelEditorCell, grid.CellToLocal((Vector3Int) position), Quaternion.identity, grid.transform);
			LevelEditorCell newLevelCell = newCell.GetComponent<LevelEditorCell>();
			newLevelCell.SetTile(tile, this);
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

	public Vector2Int ScreenToCell(Vector2 mousePosition) {
		return (Vector2Int) grid.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));
	}

	public Vector2Int WorldToCell(Vector2 worldPosition) {
		return (Vector2Int) grid.WorldToCell(worldPosition);
	}

	public void SetTool(ILevelEditorTool tool) {
		this.tool = tool;
	}

	//public Bounds GetExtents() {
	//
	//}

}
