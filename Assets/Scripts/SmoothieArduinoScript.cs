using UnityEngine;
using System.IO.Ports;
using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using System.Collections;
using UnityEditor.PackageManager.Requests;

public class SmoothieArduinoScript : MonoBehaviour
{

    public String serialPort = "COM6";
    public int numIngredients = 17;
    [Space]
    [SerializeField] private MyUIManager ui;
    [SerializeField] private SmoothieMaker smoothieMaker;

    private SerialPort sp;

    private int currentIngredient_1 = 0;
    private int currentIngredient_2 = 0;
    private int currentIngredient_3 = 0;

    public int jifCounter = 0;

    private bool wait = false;
    public bool smoothieFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        numIngredients = ui.ingredientSprites.Count;

        try
        {
            sp = new SerialPort(serialPort, 9600);
            sp.Open();
            sp.ReadTimeout = 100;
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if (jifCounter >= 100 && !wait)
        {
            Debug.Log("JIF SMOOTHIE!!!!");
            ui.Jif();
            smoothieMaker.Blend_Jif();
            wait = true;
        }
        
        if (sp.IsOpen)
        {
            try
            {
                int output = Convert.ToInt32(sp.ReadLine());
                if (!wait)
                {
                    if (output <= 0 || output > 4)
                    {
                        Debug.Log("Invalid ingredient number");
                        return;
                    }

                    if (output == 1)
                    {
                        //Debug.Log("Ingredient 1");
                        currentIngredient_1 = CycleIngredient(1, currentIngredient_1);

                    }
                    else if (output == 2)
                    {
                        //Debug.Log("Ingredient 2");
                        currentIngredient_2 = CycleIngredient(2, currentIngredient_2);
                    }
                    else if (output == 3)
                    {
                        //Debug.Log("Ingredient 3");
                        currentIngredient_3 = CycleIngredient(3, currentIngredient_3);
                    }
                    else if (output == 4)
                    {
                        if (currentIngredient_1 != 0 && currentIngredient_2 != 0 && currentIngredient_3 != 0)
                        {
                            Debug.Log("Confirm/Blend");
                            smoothieMaker.Blend(currentIngredient_1, currentIngredient_2, currentIngredient_3);
                            wait = true;
                        }
                    }
                }
                else if (wait && smoothieFinished)
                {
                    if (output >= 1 && output <= 4)
                    {
                        Reset();
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

    private void OnApplicationQuit()
    {
        sp.Close();
    }

    // Function to cycle through ingredients when a button is pressed
    public int CycleIngredient(int ingredientNum, int currentIndex)
    {
        currentIndex = (currentIndex + 1) % numIngredients;
        jifCounter++;
        ui.SetIngredientLabel(ingredientNum, currentIndex);
        return currentIndex;
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

    public void Button4()
    {
        Debug.Log("Confirm/Blend");
        smoothieMaker.Blend(currentIngredient_1, currentIngredient_2, currentIngredient_3);
        wait = true;
    }

    private void Reset()
    {
        ui.Reset();
        smoothieMaker.Reset();
        currentIngredient_1 = 0;
        currentIngredient_2 = 0;
        currentIngredient_3 = 0;

        wait = false;
        smoothieFinished = false;
    }

    public void SmoothieFinished()
    {
        smoothieFinished = true;
    }
}
