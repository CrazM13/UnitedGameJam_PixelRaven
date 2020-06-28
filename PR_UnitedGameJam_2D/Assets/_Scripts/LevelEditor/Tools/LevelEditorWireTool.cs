using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

[System.Serializable]
public class LevelEditorWireTool : ILevelEditorTool {

	public LineRenderer fakeWireRenderer;
	public Gradient badGradient;
	public Gradient goodGradient;

	private Vector2 start;
	private Vector2 end;

	private bool isWiring = false;

	public void OnMouseDown(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		/*MT*/
	}

	public void OnMouseDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		if (!isWiring) return;
		end = Camera.main.ScreenToWorldPoint(mousePosition);

		fakeWireRenderer.colorGradient = levelEditor.CellExists(cell) && levelEditor.GetTileDataAt(cell).CanBeActivated() ? goodGradient : badGradient;

		DrawWire();
	}

	public void OnMouseEndDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		Vector2Int activator = levelEditor.WorldToCell(start);

		if (levelEditor.CellExists(activator) && levelEditor.GetTileDataAt(activator).CanActivateTiles() && levelEditor.CellExists(cell) && levelEditor.GetTileDataAt(cell).CanBeActivated()) {
			levelEditor.AddWire(activator, cell);
			fakeWireRenderer.enabled = false;
		}

		start = Vector2.zero;
		end = Vector2.zero;
		fakeWireRenderer.enabled = false;
	}

	public void OnMouseStartDrag(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		if (!levelEditor.CellExists(cell) || !levelEditor.GetTileDataAt(cell).CanActivateTiles()) return;

		isWiring = true;
		start = levelEditor.CellToWorld(cell);
		end = start;
		fakeWireRenderer.enabled = true;
	}

	public void OnMouseUp(LevelEditor levelEditor, Vector2Int cell, Vector2 mousePosition) {
		fakeWireRenderer.enabled = false;
	}

	private void DrawWire() {
		fakeWireRenderer.positionCount = 200;
		float t = 0f;

		Vector2 newPoint = (start + (end - start) / 2) + Vector2.Perpendicular(end - start);

		Vector2 B;
		for (int i = 0; i < fakeWireRenderer.positionCount; i++) {
			B = (1 - t) * (1 - t) * start + 2 * (1 - t) * t * newPoint + t * t * end;
			fakeWireRenderer.SetPosition(i, B);
			t += (1 / (float) fakeWireRenderer.positionCount);
		}
	}
}
