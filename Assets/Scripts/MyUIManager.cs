using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyUIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ingredient1Label;
    [SerializeField] public TextMeshProUGUI ingredient2Label;
    [SerializeField] public TextMeshProUGUI ingredient3Label;
    [SerializeField] public TextMeshProUGUI instructionsLabel;
    [SerializeField] public TextMeshProUGUI nutritionLabel;
    [SerializeField] public GameObject nutritionObj;
    [Space]
    [SerializeField] public UnityEngine.UI.Image ingredient1Image;
    [SerializeField] public UnityEngine.UI.Image ingredient2Image;
    [SerializeField] public UnityEngine.UI.Image ingredient3Image;
    [SerializeField] public List<GameObject> splatObj = new List<GameObject>();
    [Space]
    public List<Sprite> ingredientSprites = new List<Sprite>();
    public List<Sprite> jifSprites = new List<Sprite>();
    public Sprite iceSprite;

    private String[] ingredients = {    "None", "Banana", "Strawberry", "Raspberry", 
                                        "Blueberry", "Pineapple", "Mango", "Peach", 
                                        "Spinach", "Kale", "Mint", "Honey", "Peanut Butter", 
                                        "Protein Powder", "Vanilla", "Chocolate", "Matcha" 
                                    };

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jif()
    {
        ingredient1Label.text = "Jif";
        ingredient2Label.text = "Jif";
        ingredient3Label.text = "Jif";

        ingredient1Image.sprite = jifSprites[UnityEngine.Random.Range(0, jifSprites.Count)];
        ingredient2Image.sprite = jifSprites[UnityEngine.Random.Range(0, jifSprites.Count)];
        ingredient3Image.sprite = jifSprites[UnityEngine.Random.Range(0, jifSprites.Count)];
    }
    public void SetIngredientLabel(int ingredientNum, int currentIndex)
    {
        GetComponent<AudioSource>().Play();
        if (ingredientNum == 1)
        {
            ingredient1Label.text = ingredients[currentIndex];
            ingredient1Image.sprite = ingredientSprites[currentIndex];
        }
        else if (ingredientNum == 2)
        {
            ingredient2Label.text = ingredients[currentIndex];
            ingredient2Image.sprite = ingredientSprites[currentIndex];
        }
        else if (ingredientNum == 3)
        {
            ingredient3Label.text = ingredients[currentIndex];
            ingredient3Image.sprite = ingredientSprites[currentIndex];
        }
        GetComponent<AudioSource>().Play();
    }

    public void changeInstructions(string s)
    {
        instructionsLabel.text = s;
    }

    public void Transition(Color final)
    {
        foreach (GameObject image in splatObj)
        {
            image.SetActive(true);
            image.GetComponent<SplatEffect>().Splat();
            image.GetComponent<Image>().color = final;
        }
    }

    public void StopTransition()
    {
        foreach (GameObject image in splatObj)
        {
            image.GetComponent<SplatEffect>().Stop();
            image.SetActive(false);
        }
    }

    public void DisplaySmoothie(int cal)
    {
        instructionsLabel.text = "(click any button to make another smoothie)";
        nutritionObj.SetActive(true);
        nutritionLabel.text = "Total Calories: " + cal;
        GameObject.Find("ArduinoController").GetComponent<SmoothieArduinoScript>().smoothieFinished = true;
    }

    public void Reset()
    {
        instructionsLabel.text = "Select Ingredients to make a Custom Smoothie!";
        ingredient1Label.text = ingredients[0];
        ingredient2Label.text = ingredients[0];
        ingredient3Label.text = ingredients[0];

        ingredient1Image.sprite = ingredientSprites[0];
        ingredient2Image.sprite = ingredientSprites[0];
        ingredient3Image.sprite = ingredientSprites[0];

        nutritionObj.SetActive(false);

        foreach (GameObject image in splatObj)
        {
            image.SetActive(false);
        }
    }
}
