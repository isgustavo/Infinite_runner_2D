using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHighscorePlayersBehaviour : MonoBehaviour {

    [SerializeField]
    private PlayerListVariable players;

    [SerializeField]
    private GameObject listContent = null;

    [SerializeField]
    private GameObject rowPrefab = null;

    private void OnEnable()
    {
        UpdateList();
    }

    public void UpdateList()
    {

        int recycleRow = listContent.transform.childCount;
        int i = 0;
        for (; i < players.Items.Count; i++)
        {
            UIHighscorePlayersRowBehaviour row = null;
            if (i < recycleRow)
            {
                row = listContent.transform.GetChild(i).GetComponent<UIHighscorePlayersRowBehaviour>();
                row.gameObject.SetActive(true);
            }
            else
            {
                GameObject obj = Instantiate(rowPrefab, listContent.transform);
                row = obj.GetComponent<UIHighscorePlayersRowBehaviour>();
            }

            row.SetValue(players.Items[i]);
            row.SetColor(i);
        }

        if (i < recycleRow)
        {
            for (; i < recycleRow; i++)
            {
                listContent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
