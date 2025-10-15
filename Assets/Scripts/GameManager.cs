using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject uiPrefab;
    public enum GameState { Playing, Cleared, GameOver }
    public GameState CurrentState { get; private set; }

    private void Awake(){
        // GameManagerを永続化
        DontDestroyOnLoad(gameObject);

        // UIManagerが存在しないならCanvasを生成
        if (UIManager.Instance == null){
            if (uiPrefab != null){
                var ui = Instantiate(uiPrefab);
                DontDestroyOnLoad(ui);
                Debug.Log("UIManager Canvas 生成成功");
            }else{
                Debug.LogError("GameManagerにUIプレハブが設定されていません！");
            }
        }
    }

    private void Start(){
        SetState(GameState.Playing);
    }

    public void SetState(GameState state){
        CurrentState = state;
        Debug.Log($"GameState: {state}");

        switch (state){
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Cleared:
                Time.timeScale = 0f;
                // UIManager.Instance?.ShowClearUI();
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                // UIManager.Instance?.ShowGameOverUI();
                break;
        }
    }

    public void RetryStage(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextStage()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextIndex);
    }
}