using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideBehavior : MonoBehaviour
{
    public bool stuck = false;
    public bool bodycollide = false;
    public  bool stun = false;
    public int health = 3;
    public GameObject GamePowerup;
    //private GameObject[] hearts;
    private Color original_color;
    //private bool end_game = false;

    public SpriteRenderer sr;

    //public bool End_game { get => end_game; set => end_game = value; }

    private void Start()
    {

        original_color = sr.material.color;
        //hearts = GameObject.FindGameObjectsWithTag("heart");

    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bodycollide = true;
        if (collision.collider.tag == "Platform")
        {
            stuck = true;          
        }

        if(collision.collider.tag == "Enemy")
        {
            StartCoroutine(hitPlayerAndStun());
            
        }

        //GameObject.Find("Player").GetComponent<PlayerMovement>().isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (!stun) StartCoroutine(hitPlayerAndStun());

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bodycollide = false;
        if (collision.collider.tag == "Platform") stuck = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PowerUp")
        {
            transform.parent.GetComponent<weapon>().powerup = true ;
            Destroy(GamePowerup);
        }

        if (collision.tag == "Enemy")
        {
            StartCoroutine(hitPlayerAndStun());

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemyHitInfo = collision.GetComponent<Enemy>();
        if (enemyHitInfo != null)
        {
            if (!stun) StartCoroutine(hitPlayerAndStun());

        }
    }


    public IEnumerator hitPlayerAndStun()
    {
        
        stun = true;
        health -= 1;
        Destroy(GameObject.FindGameObjectWithTag("heart"));
        StartCoroutine(changeColorPlayer());
        //transform.parent.gameObject.GetComponent<Animator>().SetBool("stun", true);
        yield return new WaitForSeconds(1f);
        //transform.parent.gameObject.GetComponent<Animator>().SetBool("stun", false);
        stun = false;
        //sr.material.color = Color.black;

    }

    IEnumerator changeColorPlayer()
    {

        sr.material.color = new Color(65f, 65f, 65f);
        yield return new WaitForSeconds(0.1f);
        sr.material.color = original_color;
        yield return new WaitForSeconds(0.1f);
        sr.material.color = new Color(65f, 65f, 65f);
        yield return new WaitForSeconds(0.1f);
        sr.material.color = original_color;
        yield return new WaitForSeconds(0.1f);
        sr.material.color = new Color(65f, 65f, 65f);
        yield return new WaitForSeconds(0.1f);
        sr.material.color = original_color;


    }








}
