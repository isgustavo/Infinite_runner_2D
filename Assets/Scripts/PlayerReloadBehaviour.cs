using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform initialPosition;
    [SerializeField]
    private BoolVariable isPause;
    [SerializeField]
    private GameEvent OnStartGameEvent;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnReloadPlayerToMenu ()
    {
        transform.position = initialPosition.position;
        animator.SetTrigger("Reload");
    }

    public void OnReloadTryAgain ()
    {
        StartCoroutine(LazyTryAgain());
    }

    IEnumerator LazyTryAgain ()
    {
        yield return new WaitForSeconds(.1f);
        isPause.SetValue(false);
        OnStartGameEvent.Raise();
    }
}
