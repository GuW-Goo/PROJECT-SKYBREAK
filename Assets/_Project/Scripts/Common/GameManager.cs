using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerHealth playerHealth;
    
    public GameState CurrentState { get; private set; }
    public float ElapsedTime { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        playerHealth.OnPlayerDied += GameOver;
    }
    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDied -= GameOver;

        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (CurrentState != GameState.Playing)
            return;

        ElapsedTime += Time.deltaTime;
    }

    public void GameReady()
    {
        CurrentState = GameState.Ready;
    }

    public void GameStart()
    {
        CurrentState = GameState.Playing;
        ElapsedTime = 0f;
    }

    public void GamePause()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    private void GameOver()
    { 
        CurrentState = GameState.GameOver;
        Debug.Log("GameOver");

        // GameOver æ¿¿¸»Ø
        GameSceneManager.Instance.LoadScene(SceneName.GameOver);

    }
}
