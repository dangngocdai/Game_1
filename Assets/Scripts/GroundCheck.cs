using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<Player>(); // laays cac bien của thằng bố nó là Player
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D conlision)
    {
        player.grounded = true;
    }

    void OnTriggerStay2D(Collider2D conllision)
    {
        player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }
}
