using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isFlipped = false;
    public List<GameObject> thingsToReset;
    public List<GameObject> thingsToDeactivate;

    private void Start()
    {
        foreach (GameObject list in thingsToReset)
        {
            list.gameObject.SetActive(true);
        }
        foreach (GameObject list in thingsToReset)
        {
            list.gameObject.SetActive(false);
        }
    }
    public void SpawnFlippedEnemies()
    {
        isFlipped = true;
        foreach (GameObject list in thingsToReset)
        {
            list.gameObject.SetActive(true);
        }
        foreach (GameObject list in thingsToDeactivate)
        {
            list.gameObject.SetActive(true);
        }
    }
}
