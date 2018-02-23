using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBehaviour : MonoBehaviour 
{
    private const string GROUND = "isGround";

    [SerializeField]
    private BoolVariable isPause;

    private SpriteRenderer sprite;
    private Animator animator;

    private Controller2DBehaviour controller;

    private float jumpVelocity;
    private float gravity;
    private Vector2 velocity;
    private float timeAfterLastFire;

    [SerializeField]
    private float moveSpeed = 6;
    [SerializeField]
    private float jumpHight = 4f;
    [SerializeField]
    private float timeToJumpApex = .4f;
    [SerializeField]
    private float timeBetweenFire = 1f;

    [SerializeField]
    private GameEvent OnGameOverEvent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isPause", isPause.Value);
    }


    void Start()
    {

        controller = GetComponent<Controller2DBehaviour>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gravity = -((2 * jumpHight) / Mathf.Pow(timeToJumpApex, 2));
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    private void FixedUpdate()
    {
        if (!isPause.Value)
        {
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }

    bool trashHit = false;
    private void Update()
    {
        if(!isPause.Value)
        {
            if (controller.GetCollisionInfo().above || controller.GetCollisionInfo().below)
            {
                velocity.y = 0;
            }


            if (Input.GetMouseButtonDown(0) && controller.GetCollisionInfo().below)
            {
                velocity.y = jumpVelocity;
            }

            velocity.y += gravity * Time.deltaTime;

            animator.SetBool(GROUND, controller.GetCollisionInfo().below);

            if (controller.GetCollisionInfo().right && trashHit)
            {
                Debug.Log("Hit");
                playerHitObstacle();
                controller.GetCollisionInfo().Reset();
                trashHit = false;
            } else
            {
                trashHit = true;
            }
        } 
    }

    private void playerHitObstacle ()
    {
        isPause.SetValue(true);
        velocity.y = 0;
        animator.SetTrigger("Hit");
        OnGameOverEvent.Raise();
    }

    public void OnPauseChanged()
    {
        animator.SetBool("isPause", isPause.Value);
    }


}
