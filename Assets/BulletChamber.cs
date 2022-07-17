using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChamber : MonoBehaviour
{

    public static BulletChamber left;
    public static BulletChamber right;

    public float reloadTime = 3f;
    public bool isLeft;

    private bool started = false;
    private float reloadDelay;

    private GameObject bullet;
    private Queue<BulletImage> bullets = new Queue<BulletImage>();
    //private Queue<BulletImage> goneBullets = new Queue<BulletImage>();

    private BulletImage lastSpawnBullet = null;

    private int magSize = -1;

    void Awake()
    {
        if(isLeft)
        {
            left = this;
        } else
        {
            right = this;
        }
        bullet = transform.GetChild(0).gameObject;
        BulletImage image = bullet.GetComponent<BulletImage>();
        StartCoroutine(StartRound(image));
    }

    void Update()
    {
        if (started && magSize < 11)
        {
            reloadDelay -= Time.deltaTime;
            if(reloadDelay < 0)
            {
                magSize++;
                //StartCoroutine(AddBullet(transform, bullet, goneBullets.Count > 0 ? goneBullets.Dequeue() : null));
                StartCoroutine(AddBullet(transform, bullet, null));
                reloadDelay = reloadTime;
            }
        }
    }

    public int Shoot()
    {
        if(bullets.Count < 1)
        {
            return -1;
        }
        BulletImage bullet = bullets.Dequeue();
        if(!bullet)
        {
            return -1;
        }

        BulletImage child = bullet.getParent();
        if (child != null)
        {
            while ((child = child.getParent()) != null)
            {
                if (child)
                {
                    child.Shift();
                }
                else
                {
                    break;
                }
            }
        }
        magSize--;
        bullet.Shoot();
        StartCoroutine(ResetBullet(bullet));
        return bullet.getDimension();
    }

    private IEnumerator ResetBullet(BulletImage bullet)
    {
        bullet.setParent(null);
        yield return new WaitForSeconds(1f);
        bullet.Move(10, isLeft);
        yield return new WaitForSeconds(1f);
        //goneBullets.Enqueue(bullet);
    }


    private IEnumerator AddBullet(Transform t, GameObject bullet, BulletImage recyle = null)
    {
        Color[] colors = Dice.instance.getColors();
        GameObject b; BulletImage image;

        if (!recyle)
        {
            b = Instantiate(bullet, t);
            image = b.GetComponent<BulletImage>();
        } else {
            b = recyle.gameObject;
            image = recyle;
        }

        b.transform.localPosition = bullet.transform.localPosition;
        b.transform.localRotation = bullet.transform.localRotation;
        b.transform.localScale = bullet.transform.localScale;

        image.Refill();
        yield return new WaitForSeconds(1f);

        int cycles = Random.Range(15, 30);
        int c = 1;
        for (int cycle = 0; cycle < Random.Range(10, 20); cycle++) {
            image.SetColor(colors[c]);
            yield return new WaitForSecondsRealtime(3f/cycles);
            c++;
            if(c > colors.Length - 1)
            {
                c = 1;
            }
        }

        int dimensionRoll = Random.Range(0, colors.Length - 2);
        image.SetColor(colors[dimensionRoll + 1]);
        image.setDimension(dimensionRoll);
        image.Move(magSize, isLeft);
        bullets.Enqueue(image);
        if(lastSpawnBullet)
        {
            image.setParent(lastSpawnBullet);
        }
        lastSpawnBullet = image;
    }

    private IEnumerator StartRound(BulletImage image)
    {
        image.Hide();
        yield return new WaitForSeconds(1f);
        image.Move(10, isLeft);
        yield return new WaitForSeconds(1f);
        started = true;
    }
}
