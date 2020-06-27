using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEditorTileSelect : MonoBehaviour {

	public string tileID = "nulltile";
	public LevelEditor levelEditor;

	private void Start() {
		GetComponent<Button>().onClick.AddListener(() => {
			levelEditor.SetTile(tileID);
		});
	}

}
