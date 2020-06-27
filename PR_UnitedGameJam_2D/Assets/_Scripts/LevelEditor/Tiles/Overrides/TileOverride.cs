using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOverride {

	private ITilePredicate[] predicates;
	private int overideSpriteID = 0;

	public TileOverride(int overideSpriteID, params ITilePredicate[] predicates) {
		this.overideSpriteID = overideSpriteID;
		this.predicates = predicates;
	}

	public bool ShouldOverride(Vector2Int position, LevelEditor levelEditor) {
		bool retFlg = true;
		foreach (ITilePredicate p in predicates) if (!p.RunPredicate(position, levelEditor)) retFlg = false;
		return retFlg;
	}

	public int GetSpriteID() {
		return overideSpriteID;
	}

}
