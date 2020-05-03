using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player; 
    // public GameObject Background1,Background2,Background3;
    // private float count1 = 1, count2 = 1, count3 =1;
    private Vector3 offset; //Private variable to store the offset distance between the player and camera
    //private Vector3 TempPosition1, TempPosition2, TempPosition3;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        Vector3 player_position = player.transform.position;
        player_position.x = 6;
        offset = transform.position - player_position;
/*        TempPosition1 = Background1.transform.localPosition;
        TempPosition2 = Background2.transform.localPosition;
        TempPosition3 = Background3.transform.localPosition;*/


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.transform.position.x >= 6)
        {
            
            Vector3 pos = transform.position;
            pos.x = player.transform.position.x + offset.x;
            transform.position = pos;
         
        }

/*        if(transform.position.x >= 50.61 * count1)
        {
            TempPosition1 = Background1.transform.localPosition;
            Background1.transform.localPosition = new Vector3(83.51f * count1, Background1.transform.localPosition.y);  
            count1++;
        }
        else if (transform.position.x < 50.61 * (count1 - 1))
        {
            Background1.transform.localPosition = TempPosition1;
            count1--;
        }
        if (transform.position.x >= 78 * count2 )
        {
            TempPosition2 = Background2.transform.localPosition;
            Background2.transform.localPosition = new Vector3(115.5f * count2, Background2.transform.localPosition.y);
            count2++;
        }
        else if (transform.position.x < 78 * (count2 - 1))
        {
            Background2.transform.localPosition = TempPosition2;
            count2--;
        }

        if (transform.position.x >= 113 * count3)
        {
            TempPosition3 = Background3.transform.localPosition;
            Background3.transform.localPosition = new Vector3(141f * count3, Background3.transform.localPosition.y);
            count3++;
        }
        else if (transform.position.x < 113 * (count3 - 1))
        {
            Background3.transform.localPosition = TempPosition3;
            count3--;
        }
*/


        //transform.position = new Vector3(player.transform.position.x + offset.x,transform.position.y);

    }
}
