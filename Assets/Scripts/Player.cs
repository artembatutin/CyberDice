using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{

    public const float SHOT_DELAY = 0.5f;

    public Transform shootPoint;
    public Transform rotateTrans;

    private float leftShotDelay = 0;
    private float rightShotDelay = 0;

    private Rigidbody2D rb;
    public int selected;
    public float speed;
    public List<string> guns = new List<string>();
    private float horizontal;
    private float vertical;
    private GunClass gun;



    void Start()
    {

        gun = GetComponent<GunClass>();
        rb = GetComponent<Rigidbody2D>();

        rb.fixedAngle = true;//idk if deprecated :D
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(selected == 0)
            {
                selected = 1;
            }
            else
            {
                selected = 0;
            }
        }
        bool leftClick = Input.GetMouseButton(0);
        bool rightClick = Input.GetMouseButton(1);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rotateTrans.transform.eulerAngles = new Vector3(0, 0, angle);
        shootPoint.transform.eulerAngles = new Vector3(0, 0, angle);


        if(leftClick && leftShotDelay < 0)
        {
            int leftBullet = BulletChamber.left.Shoot();
            if (leftBullet > 0)
            {
                gun.Shoot(shootPoint, 4, leftBullet);
                leftShotDelay = SHOT_DELAY;
            } else
            {
                leftShotDelay -= Time.deltaTime;
            }
        } else
        {
            leftShotDelay -= Time.deltaTime;
        }

        if (rightClick && rightShotDelay < 0)
        {
            int rightBullet = BulletChamber.right.Shoot();
            if (rightBullet > 0)
            {
                gun.Shoot(shootPoint, 4, rightBullet);
                rightShotDelay = SHOT_DELAY;
            } else
            {
                rightShotDelay -= Time.deltaTime;
            }
        } else
        {
            rightShotDelay -= Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
