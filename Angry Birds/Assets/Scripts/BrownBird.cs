using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : Bird
{
    private bool hasEntered = false;
    private Animator anim;

    public override void OnCollisionEnter2D(Collision2D col)
    {
        
        if(col.collider.gameObject.tag != "Environment"){
            if(!hasEntered){
                hasEntered = true;
                anim = GetComponent<Animator>();
                anim.SetBool("hasExplode", hasEntered);
                Collider.radius = 1.0f;
                transform.localScale = new Vector2(2,2);
                RigidBody.bodyType = RigidbodyType2D.Static;
            }
            Destroy(col.collider.gameObject);
        }
    }
}
