using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform Player;
    public Vector3 CamOffset;
    public Vector3 CamRotate;

    public float Rspeed = 50f;

    public float left = 0f;
    public float right = 0f;
    public float down = 0f;
    public float up = 0f;
    public float Hinput = 0f;
    public float Vinput = 0f;

    //These are "current" X and Y cords containers.
    public float cY = 0f;
    public float cX = 0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Updates camera position to follow the "player".
        //transform.position = Player.position + CamOffset;

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

        //Translates input * camera speed into X and Y cords for the cam to update to.
        cY += Vinput * Rspeed * Time.deltaTime;
        cX += Hinput * Rspeed * Time.deltaTime;
        
    }

    void LateUpdate()
    {
        //This block of code moves the camera itself, based on info I've seen online it should be in the "LateUpdate" method.
        CamOffset = new Vector3(0, 0, -15f);
        Quaternion CamRotate = Quaternion.Euler(cY, cX, 0);
        transform.position = Player.position + CamRotate * CamOffset;
        transform.LookAt(Player);
    }
}
