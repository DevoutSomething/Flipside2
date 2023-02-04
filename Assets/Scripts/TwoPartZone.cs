using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPartZone : MonoBehaviour
{
    public Transform camRestrictRightXUpY1;
    public Transform camRestrictLeftXDownY1;
    public Transform camRestrictRightXUpY2;
    public Transform camRestrictLeftXDownY2;
    public Transform transitionPoint;
    private GameObject player;
    private GameObject Camera;
    private GameObject gameManager;
    public GameObject Room;
    void Start()
    {
        Camera = GameObject.Find("Player Camera");
        player = GameObject.Find("player");
        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        if (player.transform.position.x >= transitionPoint.transform.position.x || player.transform.position.y <= transitionPoint.transform.position.y)
        {
            Room.GetComponent<ChangeZone>().camRestrictLeftxDownY = camRestrictLeftXDownY2;
            Room.GetComponent<ChangeZone>().camRestrictRightXUpY = camRestrictRightXUpY2;
        }
        else
        {
            Room.GetComponent<ChangeZone>().camRestrictLeftxDownY = camRestrictLeftXDownY1;
            Room.GetComponent<ChangeZone>().camRestrictRightXUpY = camRestrictRightXUpY1;
        }
    }
}
