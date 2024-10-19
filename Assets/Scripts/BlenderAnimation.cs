using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderAnimation : MonoBehaviour
{
    public int blendForce = 15;
    public bool blend = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBlending()
    {
        blend = true;
    }

    public void stopBlending()
    {
        blend = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (blend && obj != null)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * blendForce, ForceMode2D.Impulse);
        }
    }
}
