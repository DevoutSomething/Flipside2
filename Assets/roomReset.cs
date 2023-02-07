using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomReset : MonoBehaviour
{
    public List<GameObject> thingsToReset;
    public List<Vector3> locatons;

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

    public void restartroom()
    {
        for (int i = 0; i < thingsToReset.Count; i++)
        {
            thingsToReset[i].transform.position = locatons[i];
        }
    }
}
