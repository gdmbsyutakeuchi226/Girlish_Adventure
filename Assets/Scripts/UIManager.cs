using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
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
    }

    private void Start(){
        if (CoinManager.Instance != null){
            UpdateCoinUI(CoinManager.Instance.GetTotalCoins());
        }
    }

    public void UpdateCoinUI(int total){
        if (coinText != null)
            coinText.text = $"× {total}";
        else
            Debug.LogWarning("CoinText が設定されていません！（Prefab内でドラッグしてください）");
    }
}