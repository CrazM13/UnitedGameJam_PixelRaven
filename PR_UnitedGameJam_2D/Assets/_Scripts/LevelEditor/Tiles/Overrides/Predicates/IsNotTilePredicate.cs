using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNotTilePredicate : ITilePredicate {

	private Vector2Int offset = Vector2Int.zero;
	private string targetID = "nulltile";

	public IsNotTilePredicate(Vector2Int offset, string targetID) {
		this.offset = offset;
		this.targetID = targetID;
	}

	public bool RunPredicate(Vector2Int position, LevelEditor levelEditor) {
		return levelEditor.GetIDAt(position + offset) != targetID;
	}

}
