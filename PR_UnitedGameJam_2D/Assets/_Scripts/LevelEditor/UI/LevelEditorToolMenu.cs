using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelEditorToolMenu : MonoBehaviour {

	private LevelEditorToolSelect selectedTool = null;

	public LevelEditorToolSelect[] toolBtns;

	void Start() {

	}

	void Update() {

		foreach (LevelEditorToolSelect tool in toolBtns) {
			if (tool != selectedTool) tool.Deselect();
			else tool.Select();
		}

	}

	public void SetSelected(LevelEditorToolSelect target) {
		selectedTool = target;
	}

	public LevelEditorToolSelect GetSelected() {
		return selectedTool;
	}

}
