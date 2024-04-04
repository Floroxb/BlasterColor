using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public int damage;

    public int life;

    public List<string> color;

    public float mouvementSpeed;

    public Vector2 direction;

    public GameObject player;
    // Start is called before the first frame update

    Vector2 Normalize(Vector2 dir)
    {
        Vector2 normalized_dir = dir;
        float norm = Mathf.Sqrt(dir.x*dir.x + dir.y*dir.y);
        if (norm != 0)
        {
            normalized_dir = new Vector2(dir.x / norm, dir.y / norm);
        }
        return normalized_dir;
    }

    void setDirection(Vector2 dir)
    {
        direction = new Vector2(dir.x, dir.y);
    }

    void multiplyDirection(float x, float y)
    {
        direction.x *= x;
        direction.y *= y;
    }


    void Start()
    {
        print(player.transform.rotation.w);
        print(player.transform.rotation.x);
        print(player.transform.rotation.y);
        print(player.transform.localRotation.z);
        float angle = player.transform.rotation.z + player.transform.rotation.w;
        //angle = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        setDirection(dir);
        print($"x : {direction.x}, y : {direction.y}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collision.gameObject.GetComponent<Wall>().life--;
        }
        if (collision.gameObject.tag == "Player")
        {
            life = 0;
            collision.gameObject.GetComponent<Player>().life--;
        }
        life--;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Color
        foreach (var item in color)
        {
            if (item=="Blue")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
            }
        }

        //Move
        Vector2 currentVelocity = new Vector2(direction.x *mouvementSpeed, direction.y * mouvementSpeed);
        rb.velocity = currentVelocity;
    }
}
