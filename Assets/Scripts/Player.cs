using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 50f, maxspeed = 3, maxjump = 4, jumpPow = 220f;
    public bool grounded = true, faceright = true, doublejump = false;
    public int ourHealth, maxHealth = 5;

    public Rigidbody2D r2;
    public Animator anim;
    // Start is called before the first frame update
    void Start()//viec dau tien lam, chay 1 lan
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ourHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()// 
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x)); // gán giá trị Speed trong animator bằng giá trị thật của tốc độ(max 3) 
        //  Mathf.Abs(r2.velocity.x) trị tuyệt đối

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                doublejump = true;
                grounded = false;
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
    }

    void FixedUpdate()// tương tự như Update() nhưng sẽ đc update 0.2s và dính lứu nhiều hơn đến vật lý hơn là fun Update
    {
        float h = Input.GetAxis("Horizontal");// lấy thuộc tính Horizontal trong Input
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);// giới hạn tốc độ khi về phía bên phải

        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);// giới hạn tốc độ khi về phía bên trái
        if (r2.velocity.y > maxjump)
            r2.velocity = new Vector2(r2.velocity.x, maxjump);// giới hạn tốc độ khi về phía bên phải

        if (r2.velocity.y < -maxjump)
            r2.velocity = new Vector2(r2.velocity.x, - maxjump);// giới hạn tốc độ khi về phía bên trái

        if (h > 0 && !faceright)
        {
            Flip();
        }

        if (h < 0 && faceright)
        {
            Flip();
        }
        if (grounded)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y); //khi mà nv chạm đất thì tạo lực ma sát(giảm velocity 0.7 lan)
        }
        if (ourHealth <= 0)
        {
            Death();
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

    void Death()
    {
        ourHealth = ourHealth - 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(ourHealth <= 0)
        {

        }
    }
    
    public void Damage(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
    }

    public void Knockback(float Knocpow, Vector2 Knockdir) // hàm đẩy lùi khi chạm vào vật
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -100, Knockdir.y * Knocpow));
    }
}
