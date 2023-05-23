using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSceneManager : MonoBehaviour
{
   public void loadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
