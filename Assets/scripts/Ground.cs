using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Vector2 velocity;

    private float groundHeight;
    private float groundRight;
    public float screenRight;
    private Player player;
    
    private GameObject Obstacle;
    private GameObject Coin;
    private GameObject Shooter;

    bool didGenerateGround = false;

    private void Awake()
    {   
        Obstacle = Resources.Load("Obstacle") as GameObject;
        player = GameObject.Find("Player").GetComponent<Player>();
        screenRight = (Camera.main.transform.position.x) * 2;
        Coin = Resources.Load("Coin") as GameObject;
        Shooter = Resources.Load("Shooter") as GameObject;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        velocity = player.GroundMovement(velocity);
        pos.x -= velocity.x * Time.fixedDeltaTime;
        groundHeight = transform.position.y + (transform.localScale.y / 2);
        groundRight = transform.position.x + (transform.localScale.x / 2);
        if (groundRight < -2)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight+1)
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
        Vector2 goPos;
        goPos.y = parentPos.y;
        goPos.x = screenRight + (transform.localScale.x -1.5f)/ 2;
        GG.transform.position = goPos;

        GeneratingSpikes(GG);
        GeneratingCoins(GG);
        GeneratingShootingOnes();

    }
    //generating enemies  G = generated  
    private void GeneratingSpikes(GameObject GG)
    {
        int ObstacleNum = Random.Range(1, 4);
        for (int i = 0; i < ObstacleNum; i++)
        {
#pragma warning disable UNT0019
            GameObject GObs = Instantiate(Obstacle.gameObject);
#pragma warning restore UNT0019
            GObs.transform.parent = GG.transform;
            float y = groundHeight;
            float halfWidth = GG.GetComponent<BoxCollider2D>().transform.localScale.x / 2 - 1;
            float left = GG.transform.position.x - halfWidth;
            float right = GG.transform.position.x + halfWidth;
            float x = Random.Range(left, right);

            GObs.transform.position = new Vector2(x, y);
        }
    }
    private void GeneratingCoins(GameObject GG)
    {
        int coinNum = Random.Range(1, 2);
        for (int i = 0; i < coinNum; i++)
        {
#pragma warning disable UNT0019
            GameObject GCoin = Instantiate(Coin.gameObject);
#pragma warning restore UNT0019
            GCoin.transform.parent = GG.transform;

            float down = groundHeight;
            float up = groundHeight * 2;

            float halfWidth = GG.GetComponent<BoxCollider2D>().transform.localScale.x / 2 - 1;
            float left = GG.transform.position.x - halfWidth;
            float right = GG.transform.position.x + halfWidth;

            float y = Random.Range(down, up);
            float x = Random.Range(left, right);

            GCoin.transform.position = new Vector2(x, y);
        }
    }
    private void GeneratingShootingOnes()
    {
        int SpawnChance = Random.Range(1, 100);
        if (SpawnChance < 20 && player.currDistance > 0 && GameLogic.TimeBetwenShootingResp <= 0)
        {
            int SpawnPositionY = Random.Range(9, 23);
#pragma warning disable UNT0019
            GameObject CreatedShoter = Instantiate(Shooter.gameObject);
#pragma warning restore UNT0019
            CreatedShoter.transform.position = new Vector2(screenRight-0.5f, SpawnPositionY);
            GameLogic.TimeBetwenShootingResp = 2;
        }
    }
}
