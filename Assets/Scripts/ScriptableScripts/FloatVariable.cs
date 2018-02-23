using UnityEngine;


[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
    private float floatValue;
    public float Value
    {
        get
        {
            return floatValue;
        }
    }

    [SerializeField]
    public bool resetOnEnable;

    private void OnEnable()
    {
        if (resetOnEnable)
        {
            floatValue = 0f;
        }
    }
}
