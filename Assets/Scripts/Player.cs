using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 50f, maxspeed = 3, jumpPow = 220f;
    public bool grounded = true, faceright = true, doublejump = false;

    public Rigidbody2D r2;
    public Animator anim;
    // Start is called before the first frame update
    void Start()//viec dau tien lam, chay 1 lan
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()// 
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x)); // gán giá trị Speed trong animator bằng giá trị thật của tốc độ(max 3) 
        //  Mathf.Abs(r2.velocity.x) trị tuyệt đối

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            if (grounded)
            grounded = false;
            doublejump = true;
            r2.AddForce(Vector2.up * jumpPow);
        }
        else
        {
            if (doublejump)
            {
                doublejump = false;
                r2.velocity = new Vector2(r2.velocity.x, 0);
                r2.AddForce(Vector2.up * jumpPow * 0.7f);
            }
        }
    }

    void FixedUpdate()// tương tự như Update() nhưng sẽ đc update 0.2s và dính lứu nhiều hơn đến vật lý hơn là fun Update
    {
        float h = Input.GetAxis("Horizontal");// lấy thuộc tính Horizontal trong Input
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);// giới hạn tốc độ khi về phía bên phải

        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);// giới hạn tốc độ khi về phía bên trái

        if (h > 0 && !faceright)
        {
            Flip();
        }

        if (h < 0 && faceright)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

    }
}
