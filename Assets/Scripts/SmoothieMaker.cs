using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class SmoothieMaker : MonoBehaviour
{
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public List<Sprite> jifSprites = new List<Sprite>();
    public Sprite iceSprite;
    [Space]
    public List<Color> ingredientColors = new List<Color>();
    public List<int> ingredientCalories = new List<int>();
    [Space]
    [SerializeField] public GameObject ingredientPrefab;
    [SerializeField] public GameObject smoothieObj;
    [SerializeField] public Transform spawnLocation;
    [Space]
    [SerializeField] public AudioClip blenderSound;
    [SerializeField] public AudioClip doneSound;
    [SerializeField] public AudioClip jifTheme;
    [Space]
    [SerializeField] public MyUIManager ui;
    [SerializeField] public BlenderAnimation anim;

    private List<GameObject> spawnedIngredients = new List<GameObject>();
    private Color finalColor;
    private int calories;
    public bool jif = false;

    // Start is called before the first frame update
    void Start()
    {
        ingredientSprites = ui.ingredientSprites;
        jifSprites = ui.jifSprites;
        iceSprite = ui.iceSprite;
        smoothieObj.SetActive(false);
        GetComponent<AudioSource>().loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jif)
        {
            spawnIngredients(jifSprites[UnityEngine.Random.Range(0, jifSprites.Count)], 0.15f, 1);
        }
    }

    public void Blend_Jif()
    {
        ui.changeInstructions("Your drink is being made!");
        for (int i = 0; i < 25; i++)
        {
            spawnIngredients(jifSprites[UnityEngine.Random.Range(0, jifSprites.Count)], 0.1f, 1);
        }
        finalColor = Color.green;
        foreach (SpriteRenderer s in smoothieObj.GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = finalColor;
        }
        calories = -1;
        StartCoroutine(BlendSmoothie(true));
    }
    public void Blend(int ingredient1, int ingredient2, int ingredient3)
    {
        ui.changeInstructions("Your drink is being made!");
        spawnIngredients(ingredientSprites[ingredient1], 0.25f, UnityEngine.Random.Range(2, 4));
        spawnIngredients(ingredientSprites[ingredient2], 0.25f, UnityEngine.Random.Range(2, 4));
        spawnIngredients(ingredientSprites[ingredient3], 0.25f, UnityEngine.Random.Range(2, 4));
        spawnIngredients(iceSprite, 0.25f, 3);

        CalcSmoothieColor(ingredient1, ingredient2, ingredient3);
        SmoothieNutrition(ingredient1, ingredient2, ingredient3);
        StartCoroutine(BlendSmoothie(false));
    }

    private void spawnIngredients(Sprite s, float scale, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(ingredientPrefab, spawnLocation);
            obj.transform.position += new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-5.0f, 5.0f), 0);
            obj.GetComponent<SpriteRenderer>().sprite = s;
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.GetComponent<CircleCollider2D>().radius = 1.75f;
            spawnedIngredients.Add(obj);
        }
    }

    private IEnumerator BlendSmoothie(bool jif)
    {
        yield return new WaitForSeconds(5);
        PlayAudio(blenderSound);
        anim.startBlending();
        yield return new WaitForSeconds(8);
        GetComponent<AudioSource>().Stop();
        anim.stopBlending();

        // Transition
        ui.Transition(finalColor);
        smoothieObj.SetActive(true);
        yield return new WaitForSeconds(5);
        ui.StopTransition();
        RemoveSpawnedIngredients();

        // Show final Smoothie
        ui.DisplaySmoothie(calories);
        PlayAudio(doneSound);

        if(jif)
        {
            GetComponent<AudioSource>().loop = true;
            PlayAudio(jifTheme);
            this.jif = true;
            GameObject.Find("ArduinoController").GetComponent<SmoothieArduinoScript>().jifCounter = 0;
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    private void RemoveSpawnedIngredients()
    {
        foreach (GameObject obj in spawnedIngredients)
        {
            Destroy(obj);
        }
    }

    private void CalcSmoothieColor(int ingredient1, int ingredient2, int ingredient3)
    {
        Color c1 = ingredientColors[ingredient1];
        Color c2 = ingredientColors[ingredient2];
        Color c3 = ingredientColors[ingredient3];

        finalColor = c2 + c2 + c3 + Color.black;
        foreach (SpriteRenderer s in smoothieObj.GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = finalColor;
        }
    }

    private void SmoothieNutrition(int ingredient1, int ingredient2, int ingredient3)
    {
        calories = ingredientCalories[ingredient1] + ingredientCalories[ingredient2] + ingredientCalories[ingredient3];
    }

    public void Reset()
    {
        RemoveSpawnedIngredients();
        smoothieObj.SetActive(false);
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = false;
        jif = false;
    }

}
