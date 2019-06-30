using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator anim;
    public Rigidbody rb;
    public float speed = 20f;

    public float left = 0f;
    public float right = 0f;
    public float Hinput = 0f;

    public float forward = 0f;
    public float backward = 0f;
    public float Finput = 0f;

    public float Jspeed = 20f;
    public float jump = 0f;
    public float maxJump = 4.5f;

    public string state = "stand";
    public float lastY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for player input, allows for a combination of the keys to be pressed. Fills two variables that are later added together for the total "direction".
        //This block looks for input on player movement.
        if (Input.GetKey("a"))
        {
            left = -1f;                       
        }
        else { left = 0f;}
        if (Input.GetKey("d"))
        {
            right = 1f;
        }
        else { right = 0f; }
        if (Input.GetKey("w"))
        {
            forward = 1f;
            if (state == "stand") { anim.SetBool("walking", true); }
        }
        else { forward = 0f; anim.SetBool("walking", false); }
        if (Input.GetKey("s"))
        {
            backward = -1f;
        }
        else { backward = 0f; }

        //Allows the player to jump once they've touched the group
        if (Input.GetKeyDown("space") && state == "stand")
        {
            state = "jump";
            jump = 1f;
            lastY = transform.position.y;
            anim.SetTrigger("jumped");
            anim.SetBool("ascending", true);
        }
        //Causes the player to fall once they've reached the "max jump height"
        else if((float)transform.position.y - lastY >= maxJump || state == "fall" || state == "stand")
        {
            jump = 0f;
            anim.SetBool("ascending", false);
            if (state != "stand") { anim.SetBool("descending", true); }
        }

        //Combine left/right/forward/backward inputs to get the direction of Hinput and Finput.
        //A negative total results in left/backward movement a positive total results in right/forward movement.
        Hinput = left + right;
        Finput = forward + backward;
    }

    //Function that should only update when collision is detected. Resets player state to "stand" so they can jump again.
    //Intention is to only enable jumping when on a surface.
    void OnCollisionEnter()
    {
        //Checks that state isn't "stand" and velocity is 0 or lower before changing state to "stand".
        //This prevents it from setting the state to stand before the player leaves the ground (which would prevent the jump).
        if (state != "stand" && rb.velocity.y <= 1)
        {
            state = "stand";
            anim.SetBool("descending", false);
        }
        Debug.Log("Collided");
    }
    //Function that updates when player leaves collision. Sets state to "fall" so player cannot walk off a platform and then jump. 
    void OnCollisionExit()
    {
        //Checks that state is "stand", not "jump", and velocity is lower than 0 before changing state to "fall"
        //This prevents some oddities, such as getting a "fall" state after touching a wall.
        if (state == "stand" && state != "jump" && rb.velocity.y < 0)
        {
            state = "fall";
            anim.SetBool("descending", true);
        }
    }

    void FixedUpdate()
    {
        //Add the force to the object to give it motion, multiplies speed by input (direction) and the time delta which should cause it to behave consistently with high frame rates.
        //Does all axes at the same time.
        rb.AddForce(speed * Hinput * Time.deltaTime, Jspeed * jump * Time.deltaTime, speed * Finput * Time.deltaTime, ForceMode.VelocityChange);
    }
}
