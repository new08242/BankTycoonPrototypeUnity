using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    public GameObject pausePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PauseContinue() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PauseExit() {
        SceneManagerController.Instance.ToMainScene();
    }
}
