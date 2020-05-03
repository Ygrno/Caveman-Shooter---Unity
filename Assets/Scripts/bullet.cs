using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impact;
    
    private GameObject player;
    
    //public bool PowerUp = false;



    void Start ()
    {
        rb.velocity = transform.right * speed;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (!hitInfo.CompareTag("bodyCollider") && !hitInfo.CompareTag("MovingPlatform"))
        {
            impact = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            Enemy enemyHitInfo = hitInfo.GetComponent<Enemy>();
            Debug.Log(hitInfo);
            if (enemyHitInfo != null && !player.GetComponent<weapon>().powerup) enemyHitInfo.TakeDamage(30);
            else if (enemyHitInfo != null && player.GetComponent<weapon>().powerup) enemyHitInfo.TakeDamage(100);
            Destroy(impact, 0.30f);
            Destroy(gameObject);
        }
    }

}
