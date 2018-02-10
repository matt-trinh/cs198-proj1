using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public static bool isPaused = false;
	public GameObject pauseMenuUI;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	public void Resume() {
		isPaused = false;
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}

	public void Pause () {
		isPaused = true;
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
	}
}
