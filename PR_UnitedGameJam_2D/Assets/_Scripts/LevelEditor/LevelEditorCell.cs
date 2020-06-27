using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LevelEditorCell : MonoBehaviour {

	public SpriteRenderer spriteRenderer;

	private Tile tileData;
	private int spriteIndex = 0;

	private Vector2Int position;

	void Start() {

	}

	void Update() {

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

	public Tile GetTileData() {
		return tileData;
	}

}
