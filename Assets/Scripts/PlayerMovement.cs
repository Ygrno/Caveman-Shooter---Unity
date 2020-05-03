using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d; //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float speed; //Floating point variable to store the player's movement speed.
    public KeyCode pressUp, pressW, pressDown, pressS;
    public bool isGrounded = true;
    public bool facedRight = true;
    public bool collide_wall = false;
    public bool feetscollide = false;
    public bool isCollideWithGround = true;
    public bool isCollideWithPlatform = false, checWon = false;

    private Animator animator;
    private float face;
    private GameObject bodyCollider;
    public GameObject firePoint, wonGameLoc;
    public float jumpVelocity;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float duck_offset, stand_offset;
    public collideBehavior cb;

    public Text textP,textE;
    private int points;

    public int Points { get => points; set => points = value; }

    // Start is called before the first frame update
    void Start()
    {

        textE.enabled = false;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        face = 1;
        bodyCollider = GameObject.Find("BodyCollider");

        stand_offset = firePoint.transform.localPosition.y;
        duck_offset = firePoint.transform.localPosition.y - 0.82f;

        Points = 0;


    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        Won();

        textP.text = "Points = " + points.ToString();

        Jump();

        Duck();
        
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        Vector2 movement = new Vector2(moveHorizontal, 0f);

        

        if(!animator.GetBool("isDucking") && !bodyCollider.GetComponent<collideBehavior>().stuck)  transform.Translate(movement * Time.deltaTime);

        if (moveHorizontal < 0 && facedRight)
        {
            flip();
            facedRight = false;
        }
           
        if (moveHorizontal > 0 && !facedRight)
        {
            flip();
            facedRight = true;

        }
        animator.SetBool("isJumping", !isGrounded);


        if(cb.health <= 0 || transform.position.y < -10)
        {
            RestartGame();
        }
            
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        feetscollide = true;

        if (collision.collider.tag == "Platform")
        {
            isCollideWithPlatform = true;
        }

        if(collision.collider.tag == "Ground")
        {
            isCollideWithGround = true;
        }

/*        if(collision.collider.tag == "Enemy")
        {
            StartCoroutine (cb.hitPlayerAndStun());
        }*/

        if(isCollideWithGround && isCollideWithPlatform)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if(isCollideWithGround)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            transform.parent = collision.gameObject.transform;
            if (collision.collider.tag == "MovingPlatform")
            {
                collision.gameObject.GetComponent<move_standOn>().Player_on_platform = true;
            }
        }
        


/*        else
        {
            feetscollide = true;
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.collider.tag == "Enemy")
        {
            if (!cb.stun) StartCoroutine(cb.hitPlayerAndStun());

        }


        if (isCollideWithGround && isCollideWithPlatform)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if (isCollideWithGround)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)

    {
        feetscollide = false;
        if (collision.collider.tag == "Platform")
        {
            isCollideWithPlatform = false;
        }

        if (collision.collider.tag == "Ground")
        {
            isCollideWithGround = false;
        }

        if (!isCollideWithGround && !isCollideWithPlatform)
        {    
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
        else if(!isCollideWithPlatform)
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }


    void Won()
    {
        if(transform.position.x >= wonGameLoc.transform.position.x)
        {
            //points += 1000;
            textE.enabled = true;
            checWon = true;
        }
    }


    void Jump()
    {

        if(rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb2d.velocity.y > 0 && !(Input.GetKey(pressUp) || Input.GetKey(pressW)))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //&& !bodyColiider.GetComponent<collideBehavior>().stuck
        if ((Input.GetKeyDown(pressUp) || Input.GetKeyDown(pressW)) && isGrounded )
        {
            rb2d.velocity = Vector2.up * jumpVelocity;
           // rb2d.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);        
        }
    }

    void Duck()
    {

        if ((Input.GetKey(pressDown) || Input.GetKey(pressS)) && isGrounded && !bodyCollider.GetComponent<collideBehavior>().stuck)
        {
            animator.SetBool("isDucking", true);
            firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, duck_offset, firePoint.transform.localPosition.z);
        }
        else
        {
            animator.SetBool("isDucking", false);
            firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, stand_offset, firePoint.transform.localPosition.z);
        }
    }

    void flip()
    {
        transform.localScale = new Vector2(face = face * (-1), 1);
        firePoint.transform.Rotate(0f, 0f, 180f);
        
    }

    [System.Obsolete]
    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
