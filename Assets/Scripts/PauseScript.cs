using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour
{
    public GameObject pause;
    public bool GameIsPaused = false;

    public GameObject pauseFirstButton;
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }


        }




    }
    public void Resume()
    {
        GameIsPaused = false;
        pause.SetActive(false);
        Time.timeScale = 1;

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);


    }
    public void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        pause.SetActive(true);

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Overworld");
        Resume();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
