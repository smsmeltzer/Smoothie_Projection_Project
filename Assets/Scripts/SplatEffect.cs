using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatEffect : MonoBehaviour
{
    public float speed = 1.0f;
    private bool move = false;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    public void Splat()
    {
        GetComponent<AudioSource>().Play();
        move = true;
    }

    public void Stop()
    {
        move = false;
        transform.position = pos;
    }
}
