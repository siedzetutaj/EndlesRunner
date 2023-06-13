using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObj : MonoBehaviour
{
    private GameLogic gameLogic;
    private void Awake()
    {
        gameLogic =GameObject.Find("GameLogic").GetComponent<GameLogic>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameLogic.gold++;
            Destroy(gameObject);
        }
    }
}
