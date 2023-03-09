using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class roomReset : MonoBehaviour
{
    public List<GameObject> thingsToReset;
    public List<Vector3> locatons;
    public bool ResetRoom;
    public int RoomNum;

    private void Update()
    {
        if (ResetRoom)
        {
            ResetRoom = false;
            restartroom(RoomNum);
        }
    }
    public void restartroom(int roomNum)
    {
        foreach (Transform child in transform)
        {
            thingsToReset.Add(child.gameObject);
        }
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do somet$$anonymous$$ng
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            if(rootObjects[i].tag == "Projectile")
            {
                rootObjects[i].gameObject.SetActive(false);
            }
        }
        Debug.Log(thingsToReset);
        for (int i = 0; i < thingsToReset.Count; i++)
        {
            locatons.Add(thingsToReset[i].transform.position);
        }
        for (int i = 0; i < thingsToReset.Count; i++)
        {
            thingsToReset[i].transform.position = locatons[i];
            if (thingsToReset[i].GetComponent<Animator>() != null)
            {
                Animator anim;
                anim = thingsToReset[i].GetComponent<Animator>();
                try
                {
                    anim.SetBool("Reset", true);
                }
                catch (Exception ex)
                {
                    Debug.Log("No Reset Found in " + thingsToReset[i]);
                }
            }
            if (thingsToReset[i].GetComponent<PlantGoUp>() != null)
            {
                PlantGoUp plantGoUp = thingsToReset[i].GetComponent<PlantGoUp>();
                plantGoUp.StopAllCoroutines();
                plantGoUp.attacking = false;
            }
            else if (thingsToReset[i].CompareTag("Projectile"))
            {
                thingsToReset[i].gameObject.SetActive(false);
            }
        }
        
    }
}
