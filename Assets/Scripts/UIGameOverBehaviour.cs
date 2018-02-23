using UnityEngine;
using UnityEngine.UI;

public class UIGameOverBehaviour : MonoBehaviour 
{
    [SerializeField]
    private IntVariable currentCoinValue;
    [SerializeField]
    private PlayerListVariable players;

    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private InputField playerName;


    private void OnEnable()
    {
        coinsText.text = currentCoinValue.Value.ToString("000");
    }

    public void OnSavePlayerValue ()
    {
        Player player = new Player(playerName.text, currentCoinValue.Value);
        players.Add(player);
        playerName.text = string.Empty;
        currentCoinValue.Reset();
    }
}
