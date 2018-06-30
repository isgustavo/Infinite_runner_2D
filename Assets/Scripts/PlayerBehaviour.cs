using ODT.IR.UI;
using ODT.Scriptable;
using ODT.UI.Util;
using UnityEngine;
using UnityEngine.Events;

namespace ODT.IR
{ 
    [RequireComponent(typeof(Animator))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private static string IS_IDLE_ANIMATOR_NAME = "isIdle";
        private static string IS_GROUND_ANIMATOR_NAME = "isGround";
        private static string IS_DASH_ANIMATOR_NAME = "isDash";
        private static string IS_DEAD_ANIMATOR_NAME = "isDead";

        private enum PlayerState
        {
            Idle,
            Ground,
            Dash,
            Jump,
            Falling,
            Dead
        }

        private PlayerState playerState;
        private PlayerState State
        {
            get
            {
                return playerState;
            }
            set
            {
                switch (value)
                {
                    case PlayerState.Idle:
                        animator.SetBool(IS_IDLE_ANIMATOR_NAME, true);
                        break;
                    case PlayerState.Ground:
                        animator.SetBool(IS_IDLE_ANIMATOR_NAME, false);
                        animator.SetBool(IS_DASH_ANIMATOR_NAME, false);
                        animator.SetBool(IS_GROUND_ANIMATOR_NAME, true);
                        break;
                    case PlayerState.Dash:
                        animator.SetBool(IS_DASH_ANIMATOR_NAME, true);
                        break;
                    case PlayerState.Falling:
                        animator.SetBool(IS_GROUND_ANIMATOR_NAME, false);
                        break;
                    case PlayerState.Jump:
                        animator.SetBool(IS_GROUND_ANIMATOR_NAME, false);
                        break;
                    case PlayerState.Dead:
                        animator.SetTrigger(IS_DEAD_ANIMATOR_NAME);
                        break;
                }
                playerState = value;
            }
        }

        [SerializeField]
        private BoolVariable isPause;

        [Header("Player minimun info")]
        [SerializeField]
        private IntVariable playerScore;
        [SerializeField]
        private float playerSpeed;
        [SerializeField]
        private float playerJumpDuration;

        [Header("Collision Layers info")]
        [SerializeField]
        private LayerMask RunLayerMask;
        [SerializeField]
        private LayerMask RoofLayerMask;
        [SerializeField]
        private LayerMask DeadLayerMask;

        [Header("Events")]
        [SerializeField]
        private UnityEvent OnStartPlayEvent;
        [SerializeField]
        private UnityEvent OnStopPlayEvent;

        private Animator animator;
 
        private float playerActionTime;

        private void OnEnable()
        {
            animator = GetComponent<Animator>();

            State = PlayerState.Idle;
            playerActionTime = playerJumpDuration;
        }

        public void OnStartEvent()
        {
            State = PlayerState.Ground;
            isPause.Value = false;
            OnStartPlayEvent.Invoke();  
        }

        public void OnJumpEvent()
        {
            if (playerState == PlayerState.Ground)
            {
                State = PlayerState.Jump;
            }
        }

        private void Update()
        {
            if (isPause.Value)
            {
                return;
            }

            UpdateInput();
            
            switch (playerState)
            {
                case PlayerState.Ground:
                    Running();
                    break;
                case PlayerState.Dash:
                    Dash();
                    break;
                case PlayerState.Jump:
                    Jump();
                    break;
                case PlayerState.Falling:
                    Falling();
                    break;
                case PlayerState.Dead:
                    Dead();
                    break;
            }
        }

        private void UpdateInput()
        {
            if (UIVirtualInput.GetInput(UISwipeBehaviour.SWIPE_INPUT) == 1)
            {
                if (playerState == PlayerState.Ground)
                {
                    State = PlayerState.Jump;
                }
            }
            else if (UIVirtualInput.GetInput(UISwipeBehaviour.SWIPE_INPUT) == -1)
            {
                if (playerState == PlayerState.Ground)
                {
                    State = PlayerState.Dash;
                }
            }
        }

        private void Running()
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.red);
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, .8f, RunLayerMask);
            if (groundHit)
            {
                VerifyWall();
            }
            else
            {
                State = PlayerState.Falling;
            }
        }

        private void Dash()
        {
            RaycastHit2D roofHit = Physics2D.Raycast(transform.position, Vector2.up, 1f, RoofLayerMask);
            if (!roofHit)
            {
                State = PlayerState.Ground;
            }
        }

        private void Jump()
        {
            if (playerActionTime > 0)
            {
                transform.Translate(Vector2.up * playerSpeed * Time.deltaTime);
                playerActionTime -= Time.deltaTime;
            }
            else
            {
                State = PlayerState.Falling;
                playerActionTime = playerJumpDuration;
            }
        }

        private void Falling()
        {
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, .8f, RunLayerMask);
            if (groundHit)
            {
                State = PlayerState.Ground;
            }
            else
            {
                VerifyWall();
                transform.Translate(Vector2.down * playerSpeed * Time.deltaTime);
            }
        }

        private void VerifyWall()
        {
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right, .5f, DeadLayerMask);
            if (wallHit)
            {
                State = PlayerState.Dead;
                OnStopPlayEvent.Invoke();
            }
        }

        private void Dead()
        {
            transform.Translate(Vector2.left * playerSpeed * Time.deltaTime);
        }

       
    }
}
