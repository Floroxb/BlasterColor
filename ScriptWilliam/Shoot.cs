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

    public float impact;

    public List<GameObject> listWalls;

    public float angle;

    public Vector2 basePosition;

    public float maxDistance;

    bool destroy;

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
        direction = Normalize(dir);
    }

    float getDistance()
    {
        Vector2 distance = new Vector2(transform.position.x - basePosition.x, transform.position.y - basePosition.y);
        return Mathf.Sqrt(distance.x*distance.x + distance.y*distance.y);
    }

    void Start()
    {
        basePosition.x = transform.position.x;
        basePosition.y = transform.position.y;
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        angle = player.transform.rotation.eulerAngles.z;
        float angleInRad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(MathF.Cos(angleInRad), MathF.Sin(angleInRad));
        setDirection(dir);
        print($"x : {direction.x}, y : {direction.y}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collision.gameObject.GetComponent<Wall>().life-= damage;
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().life-= damage;
        }
        life--;

        for (int i = 0; i < color.Count; i++)
        {
            //BLUE
            //bouncingBall
            //base color blue
            if (color[i]=="Blue" && i==0 && collision.gameObject.tag == "Wall")
            {
                Vector3 baseDir = new Vector3(direction.x, direction.y, 0);
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 norm = contact.normal;
                Vector3 norm3 = new Vector3(norm.x, norm.y, 0);
                baseDir = Vector3.Reflect(baseDir, norm3);
                direction = new Vector2(baseDir.x, baseDir.y);
            }
            //Base Color not blue but contains blue
            else if (color[i] == "Blue" && i != 0 && collision.gameObject.tag == "Player")
            {
                Vector3 baseDir = new Vector3(direction.x, direction.y, 0);
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 norm = contact.normal;
                Vector3 norm3 = new Vector3(norm.x, norm.y, 0);
                baseDir = Vector3.Reflect(baseDir, norm3);
                direction = new Vector2(baseDir.x, baseDir.y);
            } else if (color[i] == "Blue" && i==0 && collision.gameObject.tag == "Player")
            {
                life = 0;
            }
            //RED
            if (color[i] == "Red")
            {
                Collider2D[] objectNear = Physics2D.OverlapCircleAll(gameObject.transform.position, impact);
                foreach (var item in objectNear)
                {
                    if (item.gameObject != gameObject)
                    {
                        if (item.gameObject.tag == "Wall")
                        {
                            item.gameObject.GetComponent<Wall>().life-= damage;
                        }
                        if (item.gameObject.tag == "Player")
                        {
                            item.gameObject.GetComponent<Player>().life-= damage;
                        }
                    }
                }
            }
            //Yellow
            if (color[i] == "Yellow" && collision.gameObject.tag == "Player")
            {
                life = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Color
        foreach (var item in color)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            if (item=="Blue")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, 128);
            }
            if (item=="Red")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(128, col.g, col.b);
            }
            if (item == "Yellow" && color[0]!="Yellow")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(128, 128, col.b);
                foreach (var wall in listWalls)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), wall.GetComponent<Collider2D>());
                }
            }
        }
        if (color.Count == 3)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Collider2D[] objectNear = Physics2D.OverlapCircleAll(gameObject.transform.position, impact);
            foreach (var item in objectNear)
            {
                if (item.gameObject != gameObject)
                {
                    if (item.gameObject.tag == "Wall")
                    {
                        item.gameObject.GetComponent<Wall>().life -= damage;
                    }
                    if (item.gameObject.tag == "Player")
                    {
                        item.gameObject.GetComponent<Player>().life-= damage;
                    }
                }
            }

        }
        if (maxDistance < getDistance())
        {
                life = 0; 
        }
        //Move
        Vector2 currentVelocity = new Vector2(direction.x *mouvementSpeed, direction.y * mouvementSpeed);
        rb.velocity = currentVelocity;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
