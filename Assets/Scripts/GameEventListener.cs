using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour 
{
    [SerializeField]
    protected GameEvent Event;
    [SerializeField]
    protected UnityEvent response;

    private void OnEnable()
    {
        if(Event != null)
        {
            Event.RegisterListener(this); 
        } else 
        {
            Debug.LogError("Register Listerer error: "+ this.name);
        }
    }

    private void OnDisable ()
    {
        if(Event != null)
        {
            Event.UnregisterListener(this);
        } else
        {
            Debug.LogError("Unregister Listerer error: " + this.name);
        }

    }

    public virtual void OnEventRaised ()
    {
        response.Invoke();
    }
}
