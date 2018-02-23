using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RuntimeLists/PlayerList")]
public class PlayerListVariable : RuntimeList<Player>
{
    
}

public class Player
{
    public string name { get; private set; }
    public int score { get; private set; }

    public Player (string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
