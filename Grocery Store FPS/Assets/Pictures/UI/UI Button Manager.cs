using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    public FirstPersonControls firstPersonControls;

    [Header("MENU SCREENS")]
    [Space(5)]
    [Header("MAIN MENUS")]
    public GameObject mainMenu;
    public GameObject controls_main_Menu;
    public GameObject selectLevelMenu;
    public GameObject attention_Level;
    public GameObject PlayLevelButtonMenu;
    public GameObject attention_Quit;
    [Space(5)]
    [Header("IN-GAME MENUS")]
    public GameObject pauseMenu;
    public GameObject controls_pause_Menu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject attention_mainMenu;
    public GameObject attention_restart;

    public GameObject RespawningInScreen;
    // MAIN MENU BUTTONS //
    public void PlayButton()
    {
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(true);
    }
    
    public void LevelSelectButton()
    {
        attention_Level.SetActive(true);
        selectLevelMenu.SetActive(false);
    }

    public void LevelSelectBack()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
    }

    public void ControlsButton()
    {
        mainMenu.SetActive(false);
        controls_main_Menu.SetActive(true);
    }

    public void ControlsBack()
    {
        mainMenu.SetActive(true);
        controls_main_Menu.SetActive(false);
    }
    public void QuitButton()
    {
        attention_Quit.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void AttentionGameYes()
    {
        mainMenu.SetActive(true);
        attention_Level.SetActive(false);
        SceneManager.LoadScene("Level 1");
    }
    public void AttentionGameNo()
    {
        attention_Level.SetActive(false);
        selectLevelMenu.SetActive(true);
    }

    public void AttentionQuitYes()
    {
        Application.Quit();
    }
    public void AttentionQuitNo()
    {
        attention_Quit.SetActive(false);
        mainMenu.SetActive(true);
    }


    /// IN GAME MENU BUTTONS

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        firstPersonControls.HUD.SetActive(false);
        firstPersonControls.canMove = false;
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        firstPersonControls.HUD.SetActive(true);
        firstPersonControls.canMove = true;
        Time.timeScale = 1;

        //
        firstPersonControls.gamePaused = false;
        //
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }


    public void InGameControls()
    {
        pauseMenu.SetActive(false);
        controls_pause_Menu.SetActive(true);
    }

    public void InGameControlsBack()
    {
        pauseMenu.SetActive(true);
        controls_pause_Menu.SetActive(false);
    }

    public void Restart()
    {
        pauseMenu.SetActive(false);
        attention_restart.SetActive(true);        
    }

    public void RestartNo()
    {
        pauseMenu.SetActive(true);
        attention_restart.SetActive(false);
    }


    public void RestartYes()
    {
        // switch cames
        //Soawn repsawn in menu
        attention_restart.SetActive(false);
        RespawningInScreen.SetActive(true);

        // Respawn countdown
        StartCoroutine(firstPersonControls.RestartSequence());

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        firstPersonControls.canMove= true;
        firstPersonControls.gamePaused= false;
    }

    public void MainMenu()
    {
        pauseMenu.SetActive(false);
      attention_mainMenu.SetActive(true);
    }

    public void MainMenuBack()
    {
        pauseMenu.SetActive(true);
        attention_mainMenu.SetActive(false);
    }

    public void MainMenuYes()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayLevel()
    {
        PlayLevelButtonMenu.SetActive(true);
    }
}
