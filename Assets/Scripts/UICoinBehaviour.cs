using UnityEngine;
using UnityEngine.UI;

public class UICoinBehaviour : MonoBehaviour 
{
    [SerializeField]
    private Text coinValue;

    [SerializeField]
    private IntVariable currentCoinValue;

    private void OnEnable()
    {
        coinValue.text = currentCoinValue.Value.ToString("000");
    }

    public void OnCurrentCoinValueChanged ()
    {
        coinValue.text = currentCoinValue.Value.ToString("000");
    }
	
}
