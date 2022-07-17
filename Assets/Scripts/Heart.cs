using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour
{
    private Player player;
    private int life = 5;
    private Dice dice;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        dice = FindObjectOfType<Dice>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (life)
        {
            case 4:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 3:
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                break;
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }

        if (player.hit)
        {
            player.hit = false;
            life--;
        }
        if (dice.hitDice)
        {
            dice.hitDice = false;
            life--;
        }
    }
}
