using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlat : MonoBehaviour
{

    public Rigidbody2D r2;
    public float timedelay = 1;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col) // check khi có 2 CollisonEnter chạm nhau
    {
        if (col.collider.CompareTag("Player")) // xác định collider có Tag là Player
        {
            StartCoroutine(fall()); // goi hàm  fall để bục rơi xuống. vì fall là funcion IEnumerator nên phải dùng StartCoroutine để gọi
        }
    }

    IEnumerator fall()
    {
        yield return new WaitForSeconds(timedelay); // tạo delay time
        r2.bodyType = RigidbodyType2D.Dynamic; // chuyển trạng thái của RigidBody về Dymanic
        yield return 0;
    }
}
