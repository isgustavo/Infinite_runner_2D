using ODT.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace ODT.IR.UI
{
    public class UIScoreBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private IntVariable scoreValue;

        public void OnUpdate()
        {
            scoreText.text = string.Format("{0} m", scoreValue.Value);
        }

    }
}
