using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform Player;
    public Vector3 CamOffset;
    public Vector3 CamRotate;

    public float Rspeed = 35f;

    public float left = 0f;
    public float right = 0f;
    public float down = 0f;
    public float up = 0f;
    public float Hinput = 0f;
    public float Vinput = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Updates camera position to follow the "player".
        transform.position = Player.position + CamOffset;

        //Check for player input, allows for a combination of the keys to be pressed. Fills two variables that are later added together for the total "direction".
        //This block is looking for input for the camera position.
        if (Input.GetKey("up"))
        {
            up = 1f;
        }
        else { up = 0f; }
        if (Input.GetKey("down"))
        {
            down = -1f;
        }
        else { down = 0f; }
        if (Input.GetKey("left"))
        {
            left = 1f;
        }
        else { left = 0f; }
        if (Input.GetKey("right"))
        {
                right = -1f;
        }
        else { right = 0f; }

        //Combine left/right/forward/backward inputs to get the direction of Hinput and Vinput.
        //A negative total results in left/backward movement a positive total results in right/forward movement.
        Hinput = left + right;
        Vinput = up + down;
        
        //WIP, this block is to allow camera rotation aroudn the "player".
        //transform.LookAt(Player);
        //CamRotate = new Vector3(Rspeed * Hinput * Time.deltaTime, Rspeed * Vinput * Time.deltaTime, 0);
        //transform.position = transform.position - CamRotate;

    }
}
