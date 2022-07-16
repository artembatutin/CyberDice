using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    private bool goForPlayer;
    private GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Bullet"))
        {
            //for now
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            player = collision.gameObject;
            goForPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            goForPlayer = false;
    }

    private void Start()
    {
        target = FindObjectOfType<Target>().gameObject;
    }
    void FixedUpdate()
    {
        if (!goForPlayer)
        {
            if (transform.position.x > target.transform.position.x)
            {
                transform.position -= new Vector3(0.025f, 0);
            }
            if (transform.position.x < target.transform.position.x)
            {
                transform.position += new Vector3(0.025f, 0);
            }
            if (transform.position.y > target.transform.position.y)
            {
                transform.position -= new Vector3(0, 0.025f);
            }
            if (transform.position.y < target.transform.position.y)
            {
                transform.position += new Vector3(0, 0.025f);
            }
        }
        else if (goForPlayer)
        {
            if (transform.position.x > player.transform.position.x)
            {
                transform.position -= new Vector3(0.025f, 0);
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.position += new Vector3(0.025f, 0);
            }
            if (transform.position.y > player.transform.position.y)
            {
                transform.position -= new Vector3(0, 0.025f);
            }
            if (transform.position.y < player.transform.position.y)
            {
                transform.position += new Vector3(0, 0.025f);
            }
        }
    }
}
