using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable : ScriptableObject 
{
    [SerializeField]
    private bool boolValue;
    public bool Value
    {
        get
        {
            return boolValue; 
        }
    }

    [SerializeField]
    public bool resetOnEnable;

    [SerializeField]
    public GameEvent OnValueChangedEvent;

    private void OnEnable()
    {
        if(resetOnEnable)
        {
            boolValue = false;
        }
    }

    public void SetValue (bool value)
    {
        this.boolValue = value;
        OnValueChangedEvent.Raise();
    }
}
