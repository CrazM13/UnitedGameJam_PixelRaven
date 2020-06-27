using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LevelEditorCell : MonoBehaviour {

	public LevelEditor levelEditor;
	public SpriteRenderer spriteRenderer;
	public GameObject lineRenderer;

	private Tile tileData;
	private int spriteIndex = 0;

	private Vector2Int position;

	private List<Wire> wires = new List<Wire>();

	void Start() {

	}

	void Update() {
		for (int i = wires.Count - 1; i >= 0; i--) {
			Tile target = levelEditor.GetTileDataAt(wires[i].target);
			if (!tileData.CanActivateTiles() || (target == null || !target.CanBeActivated())) {
				Destroy(wires[i].gameObject);
				wires.RemoveAt(i);
				continue;
			}

			DrawWire(wires[i].lineRenderer, transform.position, levelEditor.CellToWorld(wires[i].target));
		}
	}

	public void SetTile(Tile tileData, LevelEditor levelEditor) {
		this.tileData = tileData;

		UpdateTileState(levelEditor);

	}

	public void UpdateTileState(LevelEditor levelEditor) {
		spriteIndex = 0;
		spriteIndex = this.tileData.CheckOverrides(levelEditor.WorldToCell(transform.position), levelEditor);
		spriteRenderer.sprite = tileData.GetSprite(spriteIndex);
	}

	public int GetSpriteIndex() {
		return spriteIndex;
	}

	public Tile GetTileData() {
		return tileData;
	}

	void DrawWire(LineRenderer wire, Vector2 point0, Vector2 point1) {
		wire.positionCount = 200;
		float t = 0f;

		Vector2 newPoint = point0 + Vector2.Perpendicular(point1 - point0);

		Vector2 B = Vector2.zero;
		for (int i = 0; i < wire.positionCount; i++) {
			B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * newPoint + t * t * point1;
			wire.SetPosition(i, B);
			t += (1 / (float) wire.positionCount);
		}
	}

	public void AddWire(Vector2Int target) {
		wires.Add(new Wire(transform, lineRenderer, target));
	}

	public Wire[] GetConnections() {
		return wires.ToArray();
	}

	public class Wire {
		public GameObject gameObject;
		public LineRenderer lineRenderer;
		public Vector2Int target;

		public Wire(Transform parent, GameObject prefab, Vector2Int target) {
			gameObject = Instantiate(prefab, parent);

			lineRenderer = gameObject.GetComponent<LineRenderer>();

			this.target = target;
		}

	}

}
