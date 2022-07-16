using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    private const float MIN_TIMER = 10f;
    private const float MAX_TIMER = 20f;

    private const int CYCLES = 30;
    private const float DELAY_CYCLES = 0.2f;

    [SerializeField]
    public Sprite[] dicesImages;

    private SpriteRenderer sr;
    private int currentDimension = -1;

    private float waitTimer = 10f;
    private bool shuffling = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();   
    }

    void Update()
    {
        waitTimer -= Time.deltaTime;
        if(waitTimer < 0 && !shuffling)
        {
            shuffling = true;
            StartCoroutine(shuffle());
        }
    }

    public IEnumerator shuffle()
    {
        for (int cycle = 0; cycle < CYCLES; cycle++)
        {
            int random = Random.Range(0, dicesImages.Length);
            sr.sprite = dicesImages[random];
            yield return new WaitForSeconds(DELAY_CYCLES);
        }

        // todo: show text that dimension changed later

        currentDimension = Random.Range(0, dicesImages.Length);
        sr.sprite = dicesImages[currentDimension];

        waitTimer = Random.Range(MIN_TIMER, MAX_TIMER);
        shuffling = false;
    }
}
