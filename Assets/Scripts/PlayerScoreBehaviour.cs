using ODT.Scriptable;
using UnityEngine;

namespace ODT.IR
{
    public class PlayerScoreBehaviour : MonoBehaviour
    {
        [SerializeField]
        private IntVariable scoreValue;

        [Header("Events")]
        [SerializeField]
        private GameEvent OnScoreUpdateEvent;

        private bool isPlaying;
        private float timeToNextMeter;

        private void OnEnable()
        {
            isPlaying = false;
        }

        private void Update()
        {
            if(isPlaying)
            {
                if (timeToNextMeter < 0)
                {
                    scoreValue.Value += 1;
                    OnScoreUpdateEvent.Raise();
                    timeToNextMeter = 1;
                }
                else
                {
                    timeToNextMeter -= Time.deltaTime;
                }
            }   
        }

        public void OnStartEvent()
        {
            isPlaying = true;
            timeToNextMeter = 1;
        }

        public void OnStopEvent()
        {
            isPlaying = false;
        }
    }
}
