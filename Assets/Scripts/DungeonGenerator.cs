using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public string where;
    public bool spawned;
    private Variables rooms;
    public bool done;
    private HowMuch much;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spawn"))
        {
            if(collision.GetComponent<DungeonGenerator>().spawned == false && !spawned)
            {
                Instantiate(rooms.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
    private void Start()
    {
        much = FindObjectOfType<HowMuch>();
        Debug.Log(where);
        rooms = FindObjectOfType<Variables>();
        Invoke("Spawn", 0.25f);
    }
    private void Spawn()
    {
        if (!spawned)
        {
            if (where == "Right")
            {
                much.how--;
                done = true;
                int a = Random.Range(0, rooms.left.Count);
                Instantiate(rooms.left[a], new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), rooms.left[a].transform.rotation);
            }
            else if (where == "Left")
            {
                much.how--;
                done = true;
                int a = Random.Range(0, rooms.right.Count);
                Instantiate(rooms.right[a], new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), rooms.right[a].transform.rotation);
            }
            else if (where == "Top")
            {
                much.how--;
                done = true;
                int a = Random.Range(0, rooms.bottom.Count);
                Instantiate(rooms.bottom[a], new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), rooms.bottom[a].transform.rotation);
            }
            else if (where == "Bottom")
            {
                much.how--;
                done = true;
                int a = Random.Range(0, rooms.top.Count);
                Instantiate(rooms.top[a], new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), rooms.top[a].transform.rotation);
            }
            spawned = true;
        }
    }
}
