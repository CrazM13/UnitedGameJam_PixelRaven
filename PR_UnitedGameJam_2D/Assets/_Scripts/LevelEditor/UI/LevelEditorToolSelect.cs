using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class LevelEditorToolSelect : MonoBehaviour {

	public LevelEditor levelEditor;
	public LevelEditorToolMenu menu;

	public enum Tools { PAINT, ERASE, WIRE }
	public Tools selectedTool;

	public LevelEditorPaintTool paintTool = new LevelEditorPaintTool();
	public LevelEditorEraseTool eraseTool = new LevelEditorEraseTool();
	public LevelEditorWireTool wireTool = new LevelEditorWireTool();

	private Toggle toggle;

	private void Start() {
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener((bool value) => {
			if (menu.GetSelected() == this) {
				levelEditor.SetTool(null);
				menu.SetSelected(null);
				return;
			}

			switch(selectedTool) {
				case Tools.PAINT:
					levelEditor.SetTool(paintTool);
					break;
				case Tools.ERASE:
					levelEditor.SetTool(eraseTool);
					break;
				case Tools.WIRE:
					levelEditor.SetTool(wireTool);
					break;
			}
			menu.SetSelected(this);
		});
	}

	public void Select() {
		toggle.isOn = true;
	}

	public void Deselect() {
		toggle.isOn = false;
	}

}
