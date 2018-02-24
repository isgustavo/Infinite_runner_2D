using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneBehaviour : MonoBehaviour 
{
    [SerializeField]
    private string playerTag;

    [SerializeField]
    private GameEvent OnDeadZoneHitEvent;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            OnDeadZoneHitEvent.Raise();
        }
    }
}
