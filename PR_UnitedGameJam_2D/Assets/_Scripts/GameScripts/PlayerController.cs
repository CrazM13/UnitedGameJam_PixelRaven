using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private BoxCollider2D boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        moveVelocity = moveInput.normalized * speed;

        moveVelocity.y += Physics2D.gravity.y * Time.deltaTime;
        transform.Translate(moveVelocity * Time.deltaTime);

    }


    void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);       
    }


}
