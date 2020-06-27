using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

[CreateAssetMenu(fileName = "TileSpriteManager", menuName = "LevelEditor/Sprite Manager", order = 0)]
public class TileSpriteManager : ScriptableObject {
	
	[SerializeField]
	private SpriteImport[] importSprites;

	private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	public void Init() {
		foreach (SpriteImport s in importSprites) {
			sprites.Add(s.id, s.sprite);
		}
	}

	public static Sprite Load(string id) {
		return sprites[id];
	}

	[Serializable]
	public class SpriteImport {
		public string id;
		public Sprite sprite;
	}

}
