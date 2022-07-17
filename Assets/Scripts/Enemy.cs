using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health = 3;

    [SerializeField]
    private SpriteRenderer torso;

    [SerializeField]
    public SpriteRenderer[] srs;

    private Animator anim;
    private AudioSource source;

    public bool dying = false;
    public float dyingTime = 0.1f;
    public Vector3 scale;

    public int dimension;

    //private Color oldColor;
    private bool srReset = false;
    private float srDelay = 0f;

    private GameObject target;
    private bool goForPlayer;
    private GameObject player;

    private Vector3 aim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        scale = transform.localScale;
        if (Dice.instance)
        {
            target = Dice.instance.gameObject;
        }
        else
        {
            target = FindObjectOfType<Target>().gameObject;
        }

        //torso.sprite = Dice.instance.getTorso(dimension);
        
    }

    public void Update()
    {
        if(dying)
        {
            dyingTime -= Time.deltaTime;
            gameObject.transform.localScale = scale * dyingTime;
            if(dyingTime < 0)
            {
                Destroy(gameObject);
            }
            return;
        }
        if(srReset)
        {
            srDelay -= Time.deltaTime;
            if(srDelay < 0)
            {
                foreach (SpriteRenderer sr in srs)
                {
                    sr.material = Dice.instance.getDefaultMat();
                }
                srReset = false;
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(dying)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            return;
        }
        if (collision.transform.tag.Equals("Bullet"))
        {
            health--;
            source.PlayOneShot(Dice.instance.enemyHitClip);

            foreach (SpriteRenderer sr in srs)
            {
                sr.material = Dice.instance.getFlashMat();
            }
            srDelay = 0.2f;
            srReset = true;

            Bullet bullet = collision.transform.gameObject.GetComponent<Bullet>();

            if(bullet.getDimension() == dimension || Dice.instance.getDimension() == dimension)
            {
                health = -1;
            }

            if (health < 0)
            {
                dying = true;
            }
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


    void FixedUpdate()
    {
        if (!goForPlayer)
        {
            aim = target.transform.position;
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
            aim = player.transform.position;
            if (transform.position.x > player.transform.position.x)
            {
                transform.position -= new Vector3(0.06f, 0);
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.position += new Vector3(0.06f, 0);
            }
            if (transform.position.y > player.transform.position.y)
            {
                transform.position -= new Vector3(0, 0.06f);
            }
            if (transform.position.y < player.transform.position.y)
            {
                transform.position += new Vector3(0, 0.06f);
            }
        }

        if (aim != null)
        {
            float dist = Vector3.Distance(transform.position, aim);
            if (dist < 10f)
            {
                anim.SetBool("attacking", true);
            }
            else
            {
                anim.SetBool("attacking", false);
            }
        }
    }
}
