using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }
}
