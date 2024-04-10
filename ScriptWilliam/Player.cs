using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerID;
    public int life;
    public int movementSpeed = 10;
    public Rigidbody2D rb;
    public List<string> color;
    public bool isDead = false;
    public GameObject gameOver;

    // for the ball
    public List<GameObject> walls;
    public int ballDamage;
    public int ballLife;
    public float ballSpeed;
    public float ballImpact;
    public float ballMaxDistance;
    public float ballMaxTime;
    public GameObject spritePrefab;



    // Start is called before the first frame update
    void Start()
    {
        gameOver.gameObject.SetActive(false);
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        //Color
        foreach (var item in color)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            if (item == "Blue")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, 128);
            }
            if (item == "Red")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(128, col.g, col.b);
            }
            if (item == "Yellow")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(128, 128, col.b);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        foreach (var item in color)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            if (item == "Blue")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, 128);
            }
            if (item == "Red")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(128, col.g, col.b);
            }
            if (item == "Yellow")
            {
                this.GetComponent<SpriteRenderer>().color = new Color(128, 128, col.b);
            }
        }
        Vector2 velo = new Vector2(0, 0);
        if ((Input.GetKey(KeyCode.D) && PlayerID==1) || (Input.GetKey(KeyCode.L) && PlayerID == 2) || (Input.GetKey(KeyCode.RightArrow) && PlayerID == 3))
        {
            velo.x += movementSpeed;
        }
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) && PlayerID == 1) || (Input.GetKey(KeyCode.J) && PlayerID == 2) || (Input.GetKey(KeyCode.LeftArrow) && PlayerID == 3))
        {
            velo.x -= movementSpeed;
        }
        if ((Input.GetKey(KeyCode.S) && PlayerID == 1) || (Input.GetKey(KeyCode.K) && PlayerID == 2) || (Input.GetKey(KeyCode.DownArrow) && PlayerID == 3))
        {
            velo.y -= movementSpeed;
        }
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) && PlayerID == 1) || (Input.GetKey(KeyCode.I) && PlayerID == 2) || (Input.GetKey(KeyCode.UpArrow) && PlayerID == 3))
        {
            velo.y += movementSpeed;
        }
        if ((Input.GetKeyDown(KeyCode.E) && PlayerID == 1) || (Input.GetKeyDown(KeyCode.O) && PlayerID == 2) || (Input.GetKeyDown(KeyCode.RightControl) && PlayerID == 3))
        {
            GameObject shoot = Instantiate(spritePrefab);
            shoot.gameObject.name = "Shoot";
            shoot.AddComponent<Shoot>();
            shoot.AddComponent<Rigidbody2D>();
            shoot.AddComponent<PolygonCollider2D>();
            shoot.GetComponent<Rigidbody2D>().gravityScale = 0;
            shoot.GetComponent<Shoot>().rb = shoot.GetComponent<Rigidbody2D>();
            shoot.GetComponent<Shoot>().damage = ballDamage;
            shoot.GetComponent<Shoot>().life = ballLife;
            shoot.GetComponent<Shoot>().color = color;
            shoot.GetComponent<Shoot>().mouvementSpeed = ballSpeed;
            shoot.GetComponent<Shoot>().player = this.gameObject;
            shoot.GetComponent<Shoot>().impact = ballImpact;
            shoot.GetComponent<Shoot>().maxDistance = ballMaxDistance;
            shoot.GetComponent<Shoot>().maxTime = ballMaxTime;
            shoot.GetComponent<Shoot>().basePosition.x = transform.position.x +1;
            shoot.GetComponent<Shoot>().basePosition.y = transform.position.y ;
            shoot.gameObject.tag = "Ball";
            if (walls != null)
            { 
                shoot.GetComponent<Shoot>().listWalls = walls;
            }

        }


        /*else
        {
            velo = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }*/
        rb.velocity = velo;
        if (life <= 0)
        {
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameOver.gameObject.SetActive(true);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}

