using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Vector2 velocity;

    private float groundHeight;
    private float groundRight;
    private float screenRight;
    private Player player;

    private GameObject Obstacle;
    private GameObject Coin;

    bool didGenerateGround = false;

    //public TestDatabase EnemieDatabase;

    private void Awake()
    {   
        Obstacle = Resources.Load("Obstacle") as GameObject;
        player = GameObject.Find("Player").GetComponent<Player>();
        screenRight = Camera.main.transform.position.x * 2;
        Coin = Resources.Load("Coin") as GameObject;
    }
    void Update()
    {

        groundHeight = transform.position.y + (transform.localScale.y / 2);
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        velocity = player.GroundMovement(velocity);
        pos.x -= velocity.x * Time.fixedDeltaTime;
        groundRight = transform.position.x + (transform.localScale.x / 2);
        if (groundRight < -2)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                GenerateGround(pos);
            }
        }
        transform.position = pos;
    }
    void GenerateGround(Vector2 parentPos)
    {

        didGenerateGround = true;
        GameObject GG = Instantiate(gameObject);
        foreach (Transform child in GG.transform)
        {
            Destroy(child.gameObject);
        }
        BoxCollider2D goCollider = GG.GetComponent<BoxCollider2D>();
        Vector2 goPos;
        goPos.y = parentPos.y;
        goPos.x = screenRight + (transform.localScale.x -1.5f)/ 2;
        GG.transform.position = goPos;

        //generating enemies  G = generated  
        int ObstacleNum = Random.Range(1, 4);
        for (int i = 0; i < ObstacleNum; i++)
        {
            GameObject GObs = Instantiate(Obstacle.gameObject);
            GObs.transform.parent = GG.transform;
            //EnemieDatabase.AddObj(GObs);
            float y = groundHeight;
            float halfWidth = goCollider.transform.localScale.x / 2 - 1;
            float left = GG.transform.position.x - halfWidth;
            float right = GG.transform.position.x + halfWidth;
            float x = Random.Range(left, right);
            
            Vector2 ObsPos = new Vector2(x, y);
            GObs.transform.position = ObsPos;
        }
        //generating coins
        int coinNum = Random.Range(1, 2);
        for (int i = 0; i < coinNum; i++)
        {
            GameObject GCoin = Instantiate(Coin.gameObject);
            GCoin.transform.parent = GG.transform;
            
            float down = groundHeight;
            float up = groundHeight * 2;

            float halfWidth = goCollider.transform.localScale.x / 2 - 1;
            float left = GG.transform.position.x - halfWidth;
            float right = GG.transform.position.x + halfWidth;
            
            float y = Random.Range(down, up);
            float x = Random.Range(left, right);

            Vector2 ObsPos = new Vector2(x, y);
            GCoin.transform.position = ObsPos;
        }
        {

        }
    }
}
