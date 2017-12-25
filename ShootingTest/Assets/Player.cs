using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    const float basev = 0.1f;
    const float g = 0.0005f;
    public Vector3 velocity, acceleration;
    public Vector2 KeyInput;
    public bool isGrounded, jumpflag;

    private Animator animator;

    void Printlog()
    {
        Debug.Log(isGrounded);
    }

    void getInputKey()
    {
        KeyInput = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            KeyInput.x++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            KeyInput.x--;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            KeyInput.y++;
        }

        if (Input.GetKey(KeyCode.S))
        {
            KeyInput.y--;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpflag = true;
        }
        else jumpflag = false;
    }

    void move()
    {
        if (isGrounded && jumpflag)
            velocity.y = 0.12f;

        velocity.x = KeyInput.normalized.x * basev;
        velocity.z = KeyInput.normalized.y * basev;
        velocity.y += acceleration.y;

        if (KeyInput.magnitude == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }

        transform.Translate(velocity,Space.Self);

        isGrounded = transform.position.y <= 0;

        if (!isGrounded)
            acceleration.y -= g;
        else if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
            acceleration.y = 0;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    // Use this for initialization
    void Start (){
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        getInputKey();
        move();
        //Printlog();
	}
}
