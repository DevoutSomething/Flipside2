using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomReset : MonoBehaviour
{
    public List<GameObject> thingsToReset;
    public List<Vector3> locatons;
    public bool ResetRoom;
    public int RoomNum;

    void Start()
    {
        foreach (Transform child in transform)
        {
            thingsToReset.Add(child.gameObject);
        }
        Debug.Log(thingsToReset);
        for (int i = 0; i < thingsToReset.Count; i++)
        {
            locatons.Add(thingsToReset[i].transform.position);
        }    
    }
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
