using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{

    public static Player player;

    public int health = 30;

    public const float SHOT_DELAY = 0.3f;

    public Transform shootPoint;
    public Transform rotateTrans;

    private float leftShotDelay = 0;
    private float rightShotDelay = 0;
    public bool hit;

    private Rigidbody2D rb;
    private Animator anim;
    private bool animWalking = false;

    private bool flipped = false;


    public int selected;

    [SerializeField]
    public float speed;
    [SerializeField]
    public float shootSpeed = 40f;


    public List<string> guns = new List<string>();
    private float horizontal;
    private float vertical;
    private GunClass gun;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            hit = true;
        }
    }


    void Start()
    {
        player = this;
        anim = GetComponent<Animator>();
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

        bool momentum = horizontal > 0.3f || horizontal < -0.3f || vertical > 0.3f || vertical < -0.3f;

        if (!animWalking && momentum)
        {
            anim.SetBool("walking", true);
            animWalking = true;
        } else if(momentum) {
            anim.speed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
        } else if(!momentum && animWalking)
        {
            anim.SetBool("walking", false);
            anim.speed = 1f;
            animWalking = false;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rotateTrans.transform.eulerAngles = new Vector3(0, 0, angle);
        shootPoint.transform.eulerAngles = new Vector3(0, 0, angle);

        setFlipped(lookDir.x < 0);

        if(leftClick && leftShotDelay < 0)
        {
            int leftBullet = BulletChamber.left.Shoot();
            if (leftBullet >= 0)
            {
                gun.Shoot(shootPoint, shootSpeed, leftBullet);
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
            if (rightBullet >= 0)
            {
                gun.Shoot(shootPoint, shootSpeed, rightBullet);
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

    private void setFlipped(bool flipped)
    {
        if(this.flipped != flipped)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rotateTrans.localScale = new Vector3(-rotateTrans.localScale.x, rotateTrans.localScale.y, rotateTrans.localScale.z);
            this.flipped = flipped;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Spike"))
        {
            health--;
            Debug.Log(health);
        }
    }
}
