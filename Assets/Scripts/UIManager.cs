using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private Canvas canvas;
    public static UIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject); // Canvas全体を保持
        }else if (Instance != this){
            Debug.LogWarning("重複したUIManagerが検出されたため破棄");
            Destroy(gameObject);
        }
        canvas = GetComponent<Canvas>();
    }

    private void Start(){
        if (CoinManager.Instance != null){
            UpdateCoinUI(CoinManager.Instance.GetTotalCoins());
        }
    }
    private void OnEnable(){
        // シーン遷移時にもカメラを再設定
        AssignCamera();
    }

    private void OnLevelWasLoaded(int level){
        AssignCamera();
    }

    private void AssignCamera(){
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera){
            var mainCam = Camera.main;
            if (mainCam != null){
                canvas.worldCamera = mainCam;
                Debug.Log($"Canvas に {mainCam.name} を再設定しました");
            }
        }
    }

    public void UpdateCoinUI(int total){
        if (coinText != null)
            coinText.text = $"× {total}";
        else
            Debug.LogWarning("CoinText が設定されていません！（Prefab内でドラッグしてください）");
    }
}