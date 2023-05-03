using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject firstBG;
    public GameObject flipBG;
    private GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isFlipped)
        {
            firstBG.SetActive(false);
            flipBG.SetActive(true);
        }
        else
        {
            firstBG.SetActive(true);
            flipBG.SetActive(false);
        }
    }
}
