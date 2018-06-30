using ODT.UI.Util;
using UnityEngine;

namespace ODT.IR.UI
{
    public class UISwipeBehaviour : MonoBehaviour
    {
        public static string SWIPE_INPUT = "Swipe";

        private Vector3 startTouchPosition, endTouchPosition;

        private void OnEnable()
        {
            UIVirtualInput.AddInput(SWIPE_INPUT);
        }

        private void Update()
        {
#if UNITY_EDITOR
            OnUnityEditorInput();
#else
            OnTouchInput ();
#endif
        }

        private void OnUnityEditorInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;

                if (endTouchPosition.y > startTouchPosition.y)
                {
                    UIVirtualInput.UpdateInput(SWIPE_INPUT, 1);
                }
                else
                {
                    UIVirtualInput.UpdateInput(SWIPE_INPUT, -1);
                }
            }
            else
            {
                UIVirtualInput.UpdateInput(SWIPE_INPUT, 0);
            }
        }

        private void OnTouchInput ()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;

                if (endTouchPosition.y > startTouchPosition.y)
                {
                    UIVirtualInput.UpdateInput(SWIPE_INPUT, 1);
                }
                else
                {
                    UIVirtualInput.UpdateInput(SWIPE_INPUT, -1);
                }
            }
            else
            {
                UIVirtualInput.UpdateInput(SWIPE_INPUT, 0);
            }
        }
    }
}
