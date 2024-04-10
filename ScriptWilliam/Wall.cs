using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public List<GameObject> Players;
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            foreach (var player in Players)
            {
                player.GetComponent<Player>().walls.Remove(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
