using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneChangeNumber;
    private int sceneCount;

	private void Awake() {
		sceneCount = SceneManager.sceneCountInBuildSettings;
	}

	public void ChangeToLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeToAnyScene()
    {
        SceneManager.LoadScene(sceneChangeNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoseScreen()
    {
        SceneManager.LoadScene(sceneCount - 1);
    }

    public void WinScreen()
    {
        SceneManager.LoadScene(sceneCount - 2);
    }
}
