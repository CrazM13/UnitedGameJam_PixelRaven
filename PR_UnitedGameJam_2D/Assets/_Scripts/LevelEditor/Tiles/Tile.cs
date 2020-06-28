using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Tile {

	private string id;
	private bool canActivateTiles;
	private bool canBeActivated;
	private Sprite[] sprites;
	private string[] spriteIDs;

	private TileOverride[] overrides;

	public Tile(string tileData) {
		XmlDocument importData = new XmlDocument();
		importData.LoadXml(tileData);

		XmlElement root = importData["TILE"];
		id = root.GetAttribute("id");
		canActivateTiles = root.HasAttribute("activator") && bool.Parse(root.GetAttribute("activator"));
		canBeActivated = root.HasAttribute("activatable") && bool.Parse(root.GetAttribute("activatable"));

		List<string> spriteStringsToLoad = new List<string>();

		spriteStringsToLoad.Add(root["SPRITE"].GetAttribute("src"));

		XmlElement overrideRoot = root["OVERRIDES"];
		List<TileOverride> importOverrides = new List<TileOverride>();
		if (overrideRoot != null) {
			foreach (XmlElement o in overrideRoot.ChildNodes) {
				int spriteOverrrideID;
				{
					string spriteToLoad = o.GetAttribute("sprite");

					if (spriteStringsToLoad.Contains(spriteToLoad)) {
						spriteOverrrideID = spriteStringsToLoad.IndexOf(spriteToLoad);
					} else {
						spriteOverrrideID = spriteStringsToLoad.Count;
						spriteStringsToLoad.Add(spriteToLoad);
					}
				}

				List<ITilePredicate> loadPredicates = new List<ITilePredicate>();
				foreach (XmlElement p in o.ChildNodes) {
					switch (p.LocalName) {
						case "ISTILE": {
							int xOff = p.HasAttribute("xOffset") ? int.Parse(p.GetAttribute("xOffset")) : 0;
							int yOff = p.HasAttribute("yOffset") ? int.Parse(p.GetAttribute("yOffset")) : 0;
							loadPredicates.Add(new IsTilePredicate(new Vector2Int(xOff, yOff), p.GetAttribute("id")));
						}
						break;
						case "ISNOTTILE": {
							int xOff = p.HasAttribute("xOffset") ? int.Parse(p.GetAttribute("xOffset")) : 0;
							int yOff = p.HasAttribute("yOffset") ? int.Parse(p.GetAttribute("yOffset")) : 0;
							loadPredicates.Add(new IsNotTilePredicate(new Vector2Int(xOff, yOff), p.GetAttribute("id")));
						}
						break;
					}
				}
				importOverrides.Add(new TileOverride(spriteOverrrideID, loadPredicates.ToArray()));
			}
		}
		overrides = importOverrides.ToArray();

		sprites = new Sprite[spriteStringsToLoad.Count];
		spriteIDs = new string[spriteStringsToLoad.Count];
		for (int i = 0; i < spriteStringsToLoad.Count; i++) {
			sprites[i] = TileSpriteManager.Load(spriteStringsToLoad[i]);
			spriteIDs[i] = spriteStringsToLoad[i];
		}
	}

	public int CheckOverrides(Vector2Int cellPosition, LevelEditor levelEditor) {
		
		foreach (TileOverride o in overrides) {
			if (o.ShouldOverride(cellPosition, levelEditor)) return o.GetSpriteID();
		}
		
		return 0;
	}

	public bool CanActivateTiles() { return canActivateTiles; }
	public bool CanBeActivated() { return canBeActivated; }

	public string GetID() {
		return id;
	}

	public string GetSpriteID(int index) {
		return spriteIDs[index];
	}

	public Sprite GetSprite(int index) {
		return sprites[index];
	}

}
