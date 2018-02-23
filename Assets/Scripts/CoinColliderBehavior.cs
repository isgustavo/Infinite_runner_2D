using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinColliderBehavior : MonoBehaviour 
{
    private Animator animator;
    [SerializeField]
    private int coinValue = 1;
    [SerializeField]
    private IntVariable currentScore;
    [SerializeField]
    private string playerTag;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            currentScore.AddValue(coinValue);
            animator.SetTrigger("Hit");
        }
    }
}
