using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbController : MonoBehaviour {

    private Player characterCtrl;

    [SerializeField]
    private LimbController previousAttachedObject;

    [SerializeField]
    private LimbController nextAttachedObject;

    [SerializeField]
    private BodyPartType bodyPartType;

    private bool isEnabled = true;

    private void Awake()
    {
        characterCtrl = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;

   //   Debug.Log("Collision " + gameObject.name + " with layer " + gameObject.tag + " on layer " + gameObject.layer + " to " + collision.name + " with tag " + collision.gameObject.tag + " on layer " + collision.gameObject.layer);

        if (collidingObject.layer != gameObject.layer && collidingObject.tag == "Collision Point")
        {
            Animator enemyAnimator = collidingObject.GetComponentInParent<Animator>();              //TODO need to find a better way of doing this

            if (enemyAnimator)
            {
                var enemyAnimationStateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
                if(enemyAnimationStateInfo.IsName("Attacking") || enemyAnimationStateInfo.IsName("Attack right"))
                {
                    Detach();
                }
            }
        }
    }

    public void RemoveLowerComponent()
    {
        nextAttachedObject = null;
        gameObject.tag = "Collision Point";
    }

    public void Detach()
    {
        if (previousAttachedObject)
        {
            previousAttachedObject.RemoveLowerComponent();
        }

        if (nextAttachedObject)
        {
            nextAttachedObject.Detach();
        }

        if (isEnabled)
        {
            characterCtrl.NotifyCollision(bodyPartType);
            isEnabled = false;
        }
        

        if (!gameObject.GetComponent<Rigidbody2D>())
        {
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            collider.isTrigger = false;

            GameObject newParentObj = new GameObject();
            transform.SetParent(newParentObj.transform);

            newParentObj.AddComponent<Rigidbody2D>();
            gameObject.layer = 12;

            Destroy(this);
        }
    }
}
