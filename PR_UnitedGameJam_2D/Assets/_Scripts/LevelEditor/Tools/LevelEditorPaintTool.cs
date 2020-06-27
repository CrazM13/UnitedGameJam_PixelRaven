using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelEditorPaintTool : ILevelEditorTool {

	public void OnMouseDown(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		Tile selectedTile = levelEditor.GetSelectedTile();
		if (selectedTile != null) levelEditor.SetTile(cell, selectedTile);
	}

	public void OnMouseDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		Tile selectedTile = levelEditor.GetSelectedTile();
		if (selectedTile != null) levelEditor.SetTile(cell, selectedTile);
	}

	public void OnMouseEndDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		/*MT*/
	}

	public void OnMouseStartDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		/*MT*/
	}

	public void OnMouseUp(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		/*MT*/
	}
}
