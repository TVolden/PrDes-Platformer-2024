using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator Animator;
    public string Idle = "idle";
    public string Run = "run";
    public string Jump = "jump";
    public string Fall = "fall";

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Animator.Play(Idle);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > 0)
        {
            Animator.Play(Jump);
        }
        else if (rb.velocity.y < 0)
        {
            Animator.Play(Fall);
        }
        else if (rb.velocity.x != 0)
        {
            Animator.Play(Run);
        }
        else
        {
            Animator.Play(Idle);
        }
        GetComponent<SpriteRenderer>().flipX = rb.velocity.x < 0;
    }
}
