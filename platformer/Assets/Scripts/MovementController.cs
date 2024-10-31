using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    public int halfWidth = 17;
    public int halfHeight = 10;

    public float jumpForce = 5.0f;
    public float runSpeed = 5.0f;
    public bool grounded = false;
    public Rigidbody2D playerBody;

    // Initialize the inventory dictionary
    public Dictionary<string, int> pickedFruits = new Dictionary<string, int>();

    public Animator animator;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float x_dir = 0;

        // movement along the x axis
        if (Input.GetKey(KeyCode.D) && pos.x < halfWidth)
        {
            x_dir += 1;
        }
        if (Input.GetKey(KeyCode.A) && pos.x > -halfWidth)
        {
            x_dir -= 1;
        }

        // movement along the y axis 
        /*if (Input.GetKey(KeyCode.W) && pos.y < halfHeight)
        {
            pos.y += distance;
        }
        if (Input.GetKey(KeyCode.S) && pos.y > -halfHeight)
        {
            pos.y -= distance;
        }*/

        playerBody.velocity = new Vector2(x_dir * runSpeed, playerBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = !grounded;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }

        foreach (KeyValuePair<string,int> tag_Count in pickedFruits){
            Debug.Log(tag_Count.Key + ":" + tag_Count.Value);
        }
        animator.SetFloat("xSpeed", Mathf.Abs(playerBody.velocity.x));
        animator.SetFloat("yVelocity", playerBody.velocity.y);
        animator.SetBool("grounded", grounded);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("ded");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }else if(col.gameObject.tag == "Fruit") // I'm checking that the tag of the collided object is "Fruit"
        {
            string fruitTag = col.gameObject.GetComponent<FruitScript>().fruitTag; // I'm getting the fruitTag from the Fruit object's script
            if (pickedFruits.ContainsKey(fruitTag)){ // I'm checking if the inventory already contrains that fruit
                pickedFruits[fruitTag] += 1; // If yes, increment the counter for that fruit
            }else{
                pickedFruits.Add(fruitTag, 1); // If no, add the first item (fruit) and set its counter to 1
            }
            Destroy(col.gameObject); // Delete the fruit object from the scene and the memory
        }
    }
}
