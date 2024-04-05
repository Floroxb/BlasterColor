using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            foreach (var item in collision.gameObject.GetComponent<Shoot>().color)
            {
                if (item == "Yellow")
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            foreach (var item in collision.gameObject.GetComponent<Shoot>().color)
            {
                if (item == "Yellow")
                {
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
