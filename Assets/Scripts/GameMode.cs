using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{

    private int state = 0;//0 menu, 1 game, 2 credits

    private Animator anim;
    public AudioSource storyAudio;

    void Awake()
    {
        anim = GetComponent<Animator>();
        Time.timeScale = 0f;//pause
    }

    public void SetMenu()
    {
        Time.timeScale = 0f;
        state = 0;
        anim.SetInteger("mode", state);
    }

    public void StartGame()
    {
        //Camera.main.gameObject.GetComponent<AudioSource>().volume = Mathf.Lerp(0.6f, 0.1f, 1f);
        //storyAudio.enabled = true;
        state = 1;
        anim.SetInteger("mode", state);
    }

    public void InitGame()
    {
        //Camera.main.gameObject.GetComponent<AudioSource>().volume = Mathf.Lerp(0.1f, 0.6f, 1f);
        Time.timeScale = 1f;
        StartCoroutine(Dice.instance.shuffle());
        //storyAudio.enabled = false;
    }

    public void Credits()
    {
        state = 2;
        anim.SetInteger("mode", state);
    }

    public void TogglePause()
    {
        Time.timeScale = Time.timeScale > 0.7f ? 0f : 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
