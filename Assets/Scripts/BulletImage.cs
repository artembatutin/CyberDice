using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletImage : MonoBehaviour
{

    private RectTransform transform;
    private Image image;
    private int dimension;
    private BulletImage parent;

    private int slot = 0;
    private bool negative = false;

    private bool destroy = false;
    private float destroyTime = 2f;

    private Vector3 destination;

    void Awake()
    {
        transform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        destination = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime * 10f);
        if (destroy)
        {
            destroyTime -= Time.deltaTime;
            if (destroyTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Hide()
    {
        if (!transform)
        {
            Awake();
        }
        destination = new Vector3(destination.x, 100f, destination.z);
        transform.localPosition = destination;
    }

    public void Shoot()
    {
        destroy = true;
        destination = new Vector3(destination.x, 100f, destination.z);
    }

    public void Refill()
    {
        destination = new Vector3(destination.x, 10f, destination.z);
    }

    public void Shift()
    {
        Move(slot - 1, negative);
    }

    public void Move(int index, bool negativeX)
    {
        slot = index;
        negative = negativeX;
        destination = new Vector3((negativeX ? -70 : 70) + (index * 30 * (negativeX ? -1 : 1)), destination.y, destination.z);
    }

    public void SetColor(Color c)
    {
        image.color = c;
    }

    public void setDimension(int d)
    {
        this.dimension = d;
    }

    public int getDimension()
    {
        return this.dimension;
    }

    public void setParent(BulletImage parent)
    {
        this.parent = parent;
    }

    public BulletImage getParent()
    {
        return this.parent;
    }

}