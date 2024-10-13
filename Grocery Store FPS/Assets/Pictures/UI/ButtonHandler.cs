using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public FirstPersonControls firstPersonControls;
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject HUD;

    public GameObject winState;
    public GameObject loseState;
    // Start is called before the first frame update
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        HUD.SetActive(false);

    }
    public void ControlsMenu()
    {
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        firstPersonControls.gamePaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        HUD.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
    }

    public void Back()
    {
        PauseMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
    public void Die()
    {
         loseState.SetActive(true);
       Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void Win()
    {
        winState.SetActive(true);
        Time.timeScale *= 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Additive);
    }
}

