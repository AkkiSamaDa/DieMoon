using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask hideMask;
    public LayerMask demageMask;
    public float speed;
    public Vector2 forward;
    float currentSpeed;
    Rigidbody2D rb;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gameObject.layer = (int)Mathf.Log(demageMask.value, 2);
        currentSpeed = speed;
        rb.velocity = forward * currentSpeed;
        Invoke("PushObj", 5f);
    }

    private void FixedUpdate()
    {
        rb.velocity = forward * currentSpeed;
    }

     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObject.layer = (int)Mathf.Log(hideMask.value, 2);
            currentSpeed = 0f;
            rb.velocity = Vector2.zero;
            //播放击中特效
            animator.SetTrigger("hit");
        }
    }

    public void PushObj()
    {
        if(gameObject.activeInHierarchy)
            PoolMgr.Instance.PushObject("Bullet", gameObject);
    }
}
