using UnityEngine;

namespace ODT.Scriptable
{
    [CreateAssetMenu(menuName = "Variable/Int Variable")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField]
        public int Value;

        [SerializeField]
        private bool resetOnEnable = false;

        private void OnEnable()
        {
            if (resetOnEnable)
            {
                Value = 0;
            }
        }
    }
}
