using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour
{

    public float speed = 0.05f, changeDirection = -1;
    Vector3 Move ;
    public bool checkplayer = false;
    public GameObject player;
    public PauseMenu pauseComp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Move = transform.position;
        pauseComp = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PauseMenu>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseComp.pause)
        {
            transform.position = transform.position;
        }
        else
        {
            Move.x += speed;
            transform.position = Move;
        }
        

    }

    private void FixedUpdate()
    {
        if(checkplayer == true)
       {
        float X = player.transform.position.x + speed;
          player.transform.position = new Vector3(X, player.transform.position.y, player.transform.position.x);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            speed  = speed * changeDirection;
        }

        if (col.collider.CompareTag("Player"))
        {
            checkplayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            checkplayer = false;
        }
    }
}
