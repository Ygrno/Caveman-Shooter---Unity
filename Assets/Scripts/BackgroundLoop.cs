using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    //public float parallaxEffect;


    private void Start()
    {

        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;


    }


    private void Update()
    {
        float temp = (cam.transform.position.x );

       // float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos , transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }






}
