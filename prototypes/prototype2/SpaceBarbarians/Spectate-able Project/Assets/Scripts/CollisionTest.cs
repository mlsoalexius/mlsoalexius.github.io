using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{

    [SerializeField]
    private Animator currentAnimator;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided " + collision.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("triggered " + other.ToString());

        Animator enemyAnimator = other.gameObject.GetComponentInParent<Animator>();
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            currentAnimator.SetTrigger("Hurt");
            //  Debug.LogError("here again  " );
            Destroy(gameObject);
        }
        // Debug.Log("exited ");
    }
}
