using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemie : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private float TimeToShoot;
    private float TimeDelay = 2;
    private void Awake()
    {
        bullet = Resources.Load("Bullet") as GameObject;
        TimeToShoot = TimeDelay;
    }
    private void Update()
    {
        if( TimeToShoot <= 0 ) 
        {
            ShootBullet();
        }
        else
        {
            TimeToShoot -= Time.deltaTime;
        }
    }

    private void ShootBullet()
    {
        GameObject createdBullet = Instantiate(bullet.gameObject);
        createdBullet.transform.position=this.transform.position;
        Destroy( this.gameObject );
    }

}
