using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D bulletrb;

    private void Awake()
    {
        bulletrb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        bulletrb.AddForce(Vector2.left*8);
        if(transform.position.x < -3)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GettingDamage();
            Destroy(this.gameObject);
        }
    }

}
