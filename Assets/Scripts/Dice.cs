using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Dice : MonoBehaviour
{

    public static Dice instance;

    private const float MIN_TIMER = 10f;
    private const float MAX_TIMER = 20f;

    private const int CYCLES = 15;
    private const float DELAY_CYCLES = 0.2f;

    [SerializeField]
    public Sprite[] torsoEnemy;

    [SerializeField]
    public Sprite[] dicesImages;

    [SerializeField]
    public Color[] colors;

    [SerializeField]
    public VolumeProfile[] profiles;

    [SerializeField]
    public Volume volume;

    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material flashMat;


    [SerializeField]
    public AudioClip shootClip;
    [SerializeField]
    public AudioClip enemyHitClip;

    private Light2D light;

    public bool hitDice;
    private SpriteRenderer sr;
    private int currentDimension = -1;

    private float waitTimer = 10f;
    private bool shuffling = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeDimension(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeDimension(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeDimension(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeDimension(3);
        }



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
            int random = Random.Range(1, colors.Length - 1);
            sr.sprite = dicesImages[random];
            sr.gameObject.transform.Rotate(0, 0, Random.Range(10, 20));
            yield return new WaitForSeconds(DELAY_CYCLES);
        }

        // todo: show text that dimension changed later

        currentDimension = Random.Range(0, colors.Length - 1);
        changeDimension(currentDimension);

        waitTimer = Random.Range(MIN_TIMER, MAX_TIMER);
        shuffling = false;
    }

    public void changeDimension(int dimension)
    {

        //sr.color = colors[dimension+1];

        VolumeProfile profile = profiles[dimension+1];
        light.color = Color.Lerp(light.color, colors[dimension+1], 1f);
        sr.sprite = dicesImages[dimension+1];

        if (profile)
        {
            volume.profile = profile;
        }
        /*
        //profile.Remove<ChannelMixer>();
        ChannelMixer mixer = null;
        profile.TryGet<ChannelMixer>(out mixer);
        //profile.Add<ChannelMixer>(true);

        if (mixer)
        {
            float red = getRedIn(dimension);
            mixer.redOutRedIn = new ClampedFloatParameter(red, 0, 200, true);
            float green = getGreenIn(dimension);
            mixer.greenOutGreenIn = new ClampedFloatParameter(green, 0, 200, true);
            float blue = getBlueIn(dimension);
            mixer.blueOutBlueIn = new ClampedFloatParameter(blue, 0, 200, true);
            mixer.Release();//idk what this is
        }
        volume.profile = profile;*/
    
    }

    void OnApplicationQuit()
    {
        changeDimension(-1);
    }

    private float getRedIn(int dimension)
    {
        switch(dimension)
        {
            case -1:
                return 100;
            case 0:
                return 150;
            default:
                return 60;
        }
    }
    private float getGreenIn(int dimension)
    {
        switch (dimension)
        {
            case -1:
                return 100;
            case 2:
                return 150;
            default:
                return 60;
        }
    }
    private float getBlueIn(int dimension)
    {
        switch (dimension)
        {
            case -1:
                return 100;
            case 1:
                return 150;
            default:
                return 60;
        }
    }

    public Color[] getColors()
    {
        return this.colors;
    }

    public Color getColor(int dimension)
    {
        return this.colors[dimension + 1];
    }

    public Sprite getTorso(int dimension)
    {
        return this.torsoEnemy[dimension];
    }

    public Material getDefaultMat()
    {
        return this.defaultMat;
    }

    public Material getFlashMat()
    {
        return this.flashMat;
    }

    public int getDimension()
    {
        return this.currentDimension;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            hitDice = true;
        }
    }
}
