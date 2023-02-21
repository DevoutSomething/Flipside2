using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wood : MonoBehaviour
{
    public Sprite woodBroken;
    public Sprite woodNormal;
    public float TimeToBreak;
    public float TimeToErase;
    public float TimeToRespawn;
    public List<GameObject> logs;


    private void Start()
    {
        foreach (Transform child in transform)
        {
            logs.Add(child.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject log in logs)
        {
            StartCoroutine(BreakLog(log));
        }

    }

    public IEnumerator BreakLog(GameObject log)
    {
        yield return new WaitForSecondsRealtime(TimeToBreak);
        log.GetComponent<SpriteRenderer>().sprite = (woodBroken);
        StartCoroutine(EraseLog(log));
    }

    public IEnumerator EraseLog(GameObject log)
    {
        yield return new WaitForSecondsRealtime(TimeToErase);
        log.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(RespawnLog(log));


    }
    public IEnumerator RespawnLog(GameObject log)
    {
        yield return new WaitForSecondsRealtime(TimeToRespawn);
        log.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        log.GetComponent<SpriteRenderer>().sprite = (woodNormal);


    }

    public void Reset()
    {
        StopAllCoroutines();
        foreach (GameObject log in logs)
        {
            log.SetActive(true);
            log.GetComponent<SpriteRenderer>().sprite = (woodNormal);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }









}