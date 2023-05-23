using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public string winScene;
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "meleeAnim")
        {
            SceneManager.LoadScene(winScene);
            gameObject.SetActive(false);
        }
    }
        
}
