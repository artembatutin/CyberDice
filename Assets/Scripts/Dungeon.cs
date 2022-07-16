using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public GameObject room;
    public GameObject pathway;
    private Variables variable;
    private int repeat;
    private Transform yay;
    public float distant;
    private void Start()
    {
        Generate();
        yay = FindObjectOfType<CompositeCollider2D>().gameObject.transform;
        transform.SetParent(yay);
    }
    private void Generate()
    {
        if(repeat < 5)
        {
            repeat++;
            variable = FindObjectOfType<Variables>();
            if (variable.howmany > 0)
            {
                int a = Random.Range(1, 3);
                int b = Random.Range(1, 3);
                int c = Random.Range(1, 3);
                int d = Random.Range(1, 3);

                if (a == 1)
                {
                    bool used = false;
                    int e = 0;
                    while (e < variable.used.Count - 1)
                    {
                        if (variable.used[e] == transform.position + new Vector3(distant, 0))
                        {
                            used = true;
                        }
                        e++;
                    }
                    if (!used)
                    {
                        variable.howmany--;
                        variable.used.Add(transform.position + new Vector3(distant, 0, 0));
                        GameObject bruh = Instantiate(room, transform.position + new Vector3(distant, 0), Quaternion.identity, yay);
                        GameObject bruh2 = Instantiate(pathway, transform.position + new Vector3(distant/2, 0), Quaternion.identity, yay);
                        bruh2.transform.SetParent(yay);
                        bruh.transform.SetParent(yay);
                    }
                    else if (used)
                    {
                        Generate();
                    }
                }
                if (b == 1)
                {
                    bool used = false;
                    int e = 0;
                    while (e < variable.used.Count - 1)
                    {
                        if (variable.used[e] == transform.position - new Vector3(distant, 0))
                        {
                            used = true;
                        }
                        e++;
                    }
                    if (!used)
                    {
                        variable.howmany--;
                        variable.used.Add(transform.position - new Vector3(distant, 0, 0));
                        GameObject bruh = Instantiate(room, transform.position - new Vector3(distant, 0), Quaternion.identity, yay);
                        GameObject bruh2 = Instantiate(pathway, transform.position - new Vector3(distant/2, 0), Quaternion.identity, yay);
                        bruh2.transform.SetParent(yay);
                        bruh.transform.SetParent(yay);
                    }
                    else if (used)
                    {
                        Generate();
                    }
                }
                if (c == 1)
                {
                    bool used = false;
                    int e = 0;
                    while (e < variable.used.Count - 1)
                    {
                        if (variable.used[e] == transform.position + new Vector3(0, distant))
                        {
                            used = true;
                        }
                        e++;
                    }
                    if (!used)
                    {
                        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 90));
                        variable.howmany--;
                        variable.used.Add(transform.position + new Vector3(0, distant, 0));
                        GameObject bruh = Instantiate(room, transform.position + new Vector3(0, distant), Quaternion.identity, yay);
                        GameObject bruh2 = Instantiate(pathway, transform.position + new Vector3(0, distant/2), rot);
                        bruh.transform.SetParent(yay);
                        bruh2.transform.SetParent(yay);
                    }
                    else if (used)
                    {
                        Generate();
                    }
                }
                if (d == 1)
                {
                    bool used = false;
                    int e = 0;
                    while (e <= variable.used.Count - 1)
                    {
                        if (variable.used[e] == transform.position - new Vector3(0, distant))
                        {
                            used = true;
                        }
                        e++;
                    }
                    if (!used)
                    {
                        variable.howmany--;
                        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 90));
                        variable.used.Add(transform.position - new Vector3(0, distant, 0));
                        GameObject bruh = Instantiate(room, transform.position - new Vector3(0, distant), Quaternion.identity, yay);
                        GameObject bruh2 = Instantiate(pathway, transform.position - new Vector3(0, distant/2), rot);
                        bruh.transform.SetParent(yay);
                        bruh2.transform.SetParent(yay);
                    }
                    else if (used)
                    {
                        Generate();
                    }
                }
            }

        }
    }
}
