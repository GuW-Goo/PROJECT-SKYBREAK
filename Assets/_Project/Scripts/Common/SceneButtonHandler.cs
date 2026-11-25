using UnityEngine;

public class SceneButtonHandler : MonoBehaviour
{
    public void LoadMainMenu()
    {
        GameSceneManager.Instance.LoadScene(SceneName.MainMenu);
    }

    public void RestartGame()
    {
        GameSceneManager.Instance.LoadScene(SceneName.Playing);
    }

    public void LoadRanking()
    {
        GameSceneManager.Instance.LoadScene(SceneName.Ranking);
    }
}
