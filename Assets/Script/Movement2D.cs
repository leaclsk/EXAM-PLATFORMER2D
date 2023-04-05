using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer sr;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;


    float jumpForce = 12f;

    [SerializeField] float moveSpeed_horizontal = 400.0f;
    [SerializeField] bool is_jumping = false;
    [SerializeField] bool can_jump = false;
    [SerializeField] float maxFallSpeed;
    //[Range(0, 1)][SerializeField] float smooth_time = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
 
    }


    void Update()
    {
        horizontal_value = Input.GetAxis("Horizontal");

        if (horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        //animController.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (Input.GetButtonDown("Jump") && can_jump)
        {
            is_jumping = true;
            //animController.SetBool("Jumping", true);
        }


    }
    void FixedUpdate()
    {
        if (is_jumping && can_jump)
        {
            if (rb.gravityScale > 0)
            {
                is_jumping = false;
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                can_jump = false;

            }
            else if (rb.gravityScale < 0)
            {
                is_jumping = false;
                rb.AddForce(new Vector2(0, jumpForce * -1), ForceMode2D.Impulse);
                can_jump = false;

            }
        }
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        can_jump = true;
        //animController.SetBool("Jumping", false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //animController.SetBool("Jumping", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        can_jump = false;
    }
}
