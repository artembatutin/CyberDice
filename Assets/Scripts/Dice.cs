using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Dice : MonoBehaviour
{

    public static Dice instance;

    private const float MIN_TIMER = 5f;
    private const float MAX_TIMER = 10f;

    private const int CYCLES = 3;
    private const float DELAY_CYCLES = 0.2f;

    private float[] DIMENSION_HUES = { 4f, 2f, 1f };

    [SerializeField]
    public Sprite[] dicesImages;

    [SerializeField]
    public Color[] colors;

    [SerializeField]
    public VolumeProfile[] profiles;

    [SerializeField]
    public Volume volume;


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
            int random = Random.Range(0, colors.Length - 1);
            sr.color = colors[random];
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

        sr.color = colors[dimension+1];

        VolumeProfile profile = profiles[dimension+1];

        if(profile)
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
}
