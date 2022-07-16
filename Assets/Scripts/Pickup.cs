using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public Image image1;
    public Player player;
    public Image image2;
    public Image outline1;
    public Image outline2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Weapon"))
        {
            if(player.selected == 0)
            {
                image1.sprite = collision.transform.GetComponent<SpriteRenderer>().sprite;
            }
            if(player.selected == 1)
            {
                image2.sprite = collision.transform.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    private void Update()
    {

        if (player.selected == 0)
        {
            outline1.enabled = true;
        }
        if (player.selected == 1)
        {
            outline2.enabled = true;
        }
    }
}
