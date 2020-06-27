using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITilePredicate {

	bool RunPredicate(Vector2Int position, LevelEditor levelEditor);

}
