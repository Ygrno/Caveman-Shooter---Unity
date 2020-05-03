using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletprefab;
    private GameObject spawner;
    public bool powerup = false;
    public AudioSource shooting;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) shoot();    
    }


    void shoot()
    {
        shooting.Play();

        spawner = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
        //spawner.transform.parent = this.transform;
        
        Destroy(spawner, 0.85f);
        if (powerup == true)
        {
            spawner.transform.localScale = new Vector3(2, 2, 0);
            //spawner.GetComponent<bullet>().powerup = true;
        }
        else
        {
            spawner.transform.localScale = new Vector3(1, 1, 0);
        }
    }
}
