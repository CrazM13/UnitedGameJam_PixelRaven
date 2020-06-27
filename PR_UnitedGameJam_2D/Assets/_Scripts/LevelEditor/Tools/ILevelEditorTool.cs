using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelEditorTool {
	void OnMouseUp(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition);
	void OnMouseDown(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition);
	void OnMouseStartDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition);
	void OnMouseDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition);
	void OnMouseEndDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition);
}
