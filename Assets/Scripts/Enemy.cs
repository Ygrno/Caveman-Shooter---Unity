using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public Rigidbody2D rb2d;

    public GameObject deathEffect, player;
    public float speed;
    public float distance;
    public Text points;
    public Transform groundDetection;
    private bool movingRight = true;

    public Color original_color;
    public SpriteRenderer sr;
    //private int pointsNumber;


    private void Start()
    {
        original_color = sr.material.color;
        health = 100;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }




    // Update is called once per frame
    public void TakeDamage(int damage)
    {

        StartCoroutine(changeColorEnemy());
        health -= damage;
        player.GetComponent<PlayerMovement>().Points += 10;
        

        if (health <= 0)
        {
            Instantiate(deathEffect, gameObject.transform);
            Destroy(gameObject, 0.25f);
            player.GetComponent<PlayerMovement>().Points += 40;
            
        }

    }


    IEnumerator changeColorEnemy()
    {
        sr.material.color = new Color(65f, 65f, 65f);
        yield return new WaitForSeconds(0.1f);
        sr.material.color = original_color;
        yield return new WaitForSeconds(0.1f);
        sr.material.color = new Color(65f, 65f, 65f);
        yield return new WaitForSeconds(0.1f);
        sr.material.color = original_color;
    }

}




