using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
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
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        cam.Follow = transform;
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
        horizontal = Input.GetAxis("Horizontal");
       vertical = Input.GetAxis("Vertical");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
        switch (guns[selected])
        {
            case "Pistol":
                if (Input.GetMouseButtonDown(0))
                {
                    gun.Shoot(5);
                }
                break;
            case "Eagle":
                if (Input.GetMouseButtonDown(0))
                {
                    gun.Shoot(6);
                }
                break;
            case "Scar":
                if (Input.GetMouseButton(0))
                {
                    gun.Shoot(6);
                }
                break;
            case "Shotgun":
                if (Input.GetMouseButtonDown(0))
                {
                    gun.Shoot(3);
                }
                break;
            case "Sniper":
                if (Input.GetMouseButtonDown(0))
                {
                    gun.Shoot(8);
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        if(horizontal != 0 || vertical != 0)
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }
}
