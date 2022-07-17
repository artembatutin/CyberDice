using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private SpriteRenderer sr;
    public int dimension = -1;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public int getDimension()
    {
        return this.dimension;
    }

    public void setDimension(int d)
    {
        this.dimension = d;
        sr.color = Dice.instance.getColor(d);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
