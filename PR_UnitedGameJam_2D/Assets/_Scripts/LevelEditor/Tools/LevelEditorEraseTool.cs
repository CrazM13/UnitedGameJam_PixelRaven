using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorEraseTool : ILevelEditorTool {

	public void OnMouseDown(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		levelEditor.RemoveTile(cell);
	}

	public void OnMouseDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		levelEditor.RemoveTile(cell);
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
