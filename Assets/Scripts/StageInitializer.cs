/* =======================================
 * ファイル名 : StageInitializer.cs
 * 概要 : ステージ初期設定スクリプト
 * Date : 2025/10/29
 * Version : 0.01
 * ======================================= */
using UnityEngine;

public class StageInitializer : MonoBehaviour {
    [SerializeField] private GameObject uiManagerPrefab;
    [SerializeField] private GameObject fadeCanvasPrefab;
    [SerializeField] private int bgmID = -1;

    private GameObject fadeCanvasInstance;

    private void Start(){
        // UI生成
        if (uiManagerPrefab != null){
            var uiInstance = Instantiate(uiManagerPrefab);
            GameManager.Instance.RegisterUI(uiInstance.GetComponent<UIManager>());
        }

        // フェードCanvas生成
        if (fadeCanvasPrefab != null){
            fadeCanvasInstance = Instantiate(fadeCanvasPrefab);
            DontDestroyOnLoad(fadeCanvasInstance); // WorldMap遷移後も残してFadeIn可能
        }

        // BGM再生
        int id = bgmID >= 0 ? bgmID : GameManager.Instance.CurrentStage;
        GameManager.Instance.PlayBGM(id);
    }

    private void OnDestroy(){
        if (GameManager.Instance != null)
            GameManager.Instance.UnregisterUI();
    }
}
