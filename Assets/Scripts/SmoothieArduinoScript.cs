using UnityEngine;
using System.IO.Ports;
using System;

public class SmoothieArduinoScript : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM6", 9600);
    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                if (Convert.ToInt32(sp.ReadLine()) == 1)
                {
                    Debug.Log("1");
                }
                else if (Convert.ToInt32(sp.ReadLine()) == 2)
                {
                    Debug.Log("2");
                }
                else if (Convert.ToInt32(sp.ReadLine()) == 3)
                {
                    Debug.Log("3");
                }
                else if (Convert.ToInt32(sp.ReadLine()) == 4)
                {
                    Debug.Log("4");
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
}
