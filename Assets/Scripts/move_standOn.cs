using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_standOn : MonoBehaviour
{



    
    public Transform[] start_waypoints;

    public Transform[] player_on_waypoints;

    private Transform[] current_waypoints;

    [SerializeField]
    private bool ocotopus_died = false, firstTime = true;


    [SerializeField]
    private float moveSpeed = 5f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    public bool Ocotopus_died { get => ocotopus_died; set => ocotopus_died = value; }
    public bool Player_on_platform { get => player_on_platform; set => player_on_platform = value; }

    [SerializeField]
    private bool player_on_platform, stopped = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set position of Enemy as position of the first waypoint
        Player_on_platform = false;
        Ocotopus_died = false;
        transform.position = start_waypoints[waypointIndex].transform.position;
        current_waypoints = start_waypoints;
        
    }

    private IEnumerator Move()
    {


        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= current_waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
                current_waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == current_waypoints[waypointIndex].transform.position)
            {
                if (!player_on_platform)
                {
                    stopped = true;
                    yield return new WaitForSeconds(0.65f);
                }
                stopped = false;
                waypointIndex += 1;
            }
        }
        else if (!player_on_platform)
        {
            //yield return new WaitForSeconds(0.65f);
            waypointIndex = 0;

        }

        else if (player_on_platform)
        {
            if (firstTime)
            {
                this.waypointIndex = 0;
                firstTime = false;
            }
            current_waypoints = player_on_waypoints;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        if (Ocotopus_died && stopped == false)
        {
            StartCoroutine (Move());
        }
        

    }
}
