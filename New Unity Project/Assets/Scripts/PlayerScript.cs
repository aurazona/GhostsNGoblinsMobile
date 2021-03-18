using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int Speed = 5;
    public int Life;
    public int Gravity = 5;
    public GameObject player;
    public Rigidbody2D PlayerRb;
    public int JumpForce = 5;
    public bool IsOnGround;
    public static bool IsRight;
    public GameObject Spear;
    public Transform PlayerPos;
    public Animator PlayerAnim;
    public bool IsThrowing;
    public int ThrowCounter;
    public Collider2D PlayerCollider;
    public GameObject Self;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        PlayerCollider = GetComponent<Collider2D>();
        Physics2D.gravity *= Gravity;
        Life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        LifeCheck();
        AnimCounters();
        Movement();
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.D) | (RightTouchScript.isPressed == true))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //flips the character to face the right
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            IsRight = true;
            PlayerAnim.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.A) | (LeftTouchScript.isPressed == true))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //flips the character to face the left
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            IsRight = false;
            PlayerAnim.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.Space) | (JumpTouchScript.isPressed == true) && (IsOnGround == true))
        {
            PlayerRb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
            IsOnGround = false;
            PlayerAnim.SetBool("IsJumping", true);
            PlayerCollider.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) | (ThrowTouchScript.isPressed == true))
        {
            PlayerAnim.SetBool("IsThrowing", true);
            IsThrowing = true;
            GameObject SpearClone;
            PlayerPos = player.transform;
            SpearClone = Instantiate(Spear, transform.position, transform.rotation);
        }
        if (PlayerRb.velocity.y < -0.1)
        {
            PlayerCollider.enabled = true;
            PlayerAnim.SetBool("IsJumping", false);
            PlayerAnim.SetBool("IsFalling", true);
        }
        if(!Input.GetKey(KeyCode.A) & (!Input.GetKey(KeyCode.D))) //| (LeftTouchScript.isPressed == false) & (RightTouchScript.isPressed == false))
        {
            PlayerAnim.SetBool("IsWalking", false);
        }
    }

    public void AnimCounters()
    {
        if(IsThrowing == true)
        {
            ThrowCounter++;
        }
        if(ThrowCounter == 5)
        {
            PlayerAnim.SetBool("IsThrowing", false);
            IsThrowing = false;
            ThrowCounter = 0;
        }
    }

    public void LifeCheck()
    {
        if(Life <= 0)
        {
            Destroy(Self);
            Debug.Log("Player is dead.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            //if (PlayerRb.velocity.y > 0.1)
            //{
                //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), (PlayerRb.velocity.y > 0.1));
                //Debug.Log("Not colliding with platforms.");
            //}
            //else
            //{
                IsOnGround = true;
                PlayerAnim.SetBool("IsFalling", false);
            //}
        }
        if (collision.gameObject.tag.Equals("Hazard"))
        {
            Life--;
        }
    }
}
