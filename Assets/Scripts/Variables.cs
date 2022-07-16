using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Variables : MonoBehaviour
{
    public List<GameObject> left;
    public List<GameObject> right;
    public List<GameObject> top;
    public List<GameObject> bottom;
    public List<Vector3> used;
    public GameObject closedRoom;
    public int howmany;
    private Vector3 start;
    private Vector3 end;
    public GameObject player;
    public void GenerateBoe()
    {
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        Invoke("Choose", 0.05f);
    }
    void Choose()
    {
        if(used.Count > 0)
        {
            start = used[Random.Range(0, used.Count)];
            end = used[Random.Range(0, used.Count)];
            if (start == end)
            {
                Choose();
            }
            else
            {
                Instantiate(player, start, Quaternion.identity);
            }
        }
        else
        {
            Invoke("Choose", 0.05f);
        }
    }


}
