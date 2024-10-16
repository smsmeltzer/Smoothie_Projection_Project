using UnityEngine;
using System.IO.Ports;
using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class SmoothieArduinoScript : MonoBehaviour
{
    //SerialPort sp = new SerialPort("COM6", 9600);

    public List<String> ingredients = new List<String>();
    private int currentIngredient_1 = 0;
    private int currentIngredient_2 = 0;
    private int currentIngredient_3 = 0;

    public List<Sprite> ingredientSprites = new List<Sprite>();

    public List<String> selectedIngredients = new List<String>();

    [SerializeField] public TextMeshProUGUI ingredient1Label;
    [SerializeField] public TextMeshProUGUI ingredient2Label;
    [SerializeField] public TextMeshProUGUI ingredient3Label;
    [SerializeField] public UnityEngine.UI.Image ingredient1Image;
    [SerializeField] public UnityEngine.UI.Image ingredient2Image;
    [SerializeField] public UnityEngine.UI.Image ingredient3Image;
    [SerializeField] private Sprite jifSprite;

    private bool blendClicked = false;

    public int jifCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //sp.Open();
        //sp.ReadTimeout = 100;

        ingredient1Label.text = ingredients[0];
        ingredient2Label.text = ingredients[0];
        ingredient3Label.text = ingredients[0];

        ingredient1Image.sprite = ingredientSprites[0];
        ingredient2Image.sprite = ingredientSprites[0];
        ingredient3Image.sprite = ingredientSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (jifCounter >= 100)
        {
            Debug.Log("JIF SMOOTHIE!!!!");
            ingredient1Label.text = "Jif";
            ingredient2Label.text = "Jif";
            ingredient3Label.text = "Jif";

            ingredient1Image.sprite = jifSprite;
            ingredient2Image.sprite = jifSprite;
            ingredient3Image.sprite = jifSprite;
        }
        /*
        if (sp.IsOpen)
        {
            try
            {
                if (Convert.ToInt32(sp.ReadLine()) == 1)
                {
                    //Ingredient 1
                    Debug.Log("Ingredient 1");
                    currentIngredient_1 = CycleIngredient(1, currentIngredient_1);

                }
                else if (Convert.ToInt32(sp.ReadLine()) == 2)
                {
                    //Ingredient 2
                    Debug.Log("Ingredient 2");
                    currentIngredient_2 = CycleIngredient(2, currentIngredient_2);
                }
                else if (Convert.ToInt32(sp.ReadLine()) == 3)
                {
                    //Ingredient 3
                    Debug.Log("Ingredient 3");
                    currentIngredient_3 = CycleIngredient(3, currentIngredient_3);
                }
                else if (Convert.ToInt32(sp.ReadLine()) == 4)
                {
                    //Confirm button
                    Debug.Log("Confirm/Blend");
                    blend();
                }
            }
            catch (System.Exception)
            {
            }
        }
        */
        
    }

    private void OnApplicationQuit()
    {
        //sp.Close();
    }

    // Function to cycle through ingredients when a button is pressed
    public int CycleIngredient(int ingredientNum, int currentIndex)
    {
        if (ingredientNum <= 0 || ingredientNum >= 4)
        {
            Debug.Log("Invalid ingredient number");
            return -1;
        }
        // Increment the index and loop back to the start if needed
        currentIndex++;
        jifCounter++;
        if (currentIndex >= ingredients.Count)
        {
            currentIndex = 0;
        }

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

        return currentIndex;
    }

    public void blend()
    {  
        if (!blendClicked) //can only press blend button once to confirm selection of ingredients
        {
            blendClicked = true;
            if (jifCounter >= 100)
            {
                selectedIngredients.Add("Jif");
                selectedIngredients.Add("Jif");
                selectedIngredients.Add("Jif");
            }
            else
            {
                //Save current selection of ingredients
                selectedIngredients.Add(ingredients[currentIngredient_1]);
                selectedIngredients.Add(ingredients[currentIngredient_2]);
                selectedIngredients.Add(ingredients[currentIngredient_3]);
            }

            //Show animation of ingredients dropping into cup
            
            
            //Blend ingredients -> show smoothie color
            
            
            //Calculate and show nutrition facts
            
        }

       
    }

    //Testing with buttons instead of arduino functions
    public void Button1()
    {
        Debug.Log("Ingredient 1");
        currentIngredient_1 = CycleIngredient(1, currentIngredient_1);
    }

    public void Button2()
    {
        Debug.Log("Ingredient 2");
        currentIngredient_2 = CycleIngredient(2, currentIngredient_2);
    }

    public void Button3()
    {
        Debug.Log("Ingredient 3");
        currentIngredient_3 = CycleIngredient(3, currentIngredient_3);
    }
}
