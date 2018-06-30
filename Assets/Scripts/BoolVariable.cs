using UnityEngine;

namespace ODT.Scriptable
{
    [CreateAssetMenu(menuName = "Variable/Bool Variable")]
    public class BoolVariable : ScriptableObject
    {
        [SerializeField]
        public bool Value;

        [SerializeField]
        private bool initialValue;
        [SerializeField]
        public bool resetOnEnable;

        private void OnEnable()
        {
            if (resetOnEnable)
            {
                Value = initialValue;
            }
        }
    }
}
