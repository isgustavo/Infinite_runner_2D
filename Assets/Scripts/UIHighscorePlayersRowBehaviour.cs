using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHighscorePlayersRowBehaviour : MonoBehaviour 
{

    [SerializeField]
    private Text playerName;
    [SerializeField]
    private Text playerScore;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    internal void SetValue(Player player)
    {
        playerName.text = player.name;
        playerScore.text = player.score.ToString("000");
    }

    internal void SetColor(int i)
    {
        if(i % 2 == 0)
        {
            image.color = new Color(image.color.r, image.color.g + 30, image.color.b);
        }
    }
}
