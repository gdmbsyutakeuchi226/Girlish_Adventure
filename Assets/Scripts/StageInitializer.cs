/* =======================================
 * ファイル名 : StageInitializer.cs
 * 概要 : ステージ初期設定スクリプト
 * Date : 2025/10/29
 * Version : 0.01
 * ======================================= */
using UnityEngine;

public class StageInitializer : MonoBehaviour {
    [SerializeField] private GameObject uiManagerPrefab;
    [SerializeField] private int bgmID = -1; // ステージ固有BGM（任意指定）

    private GameObject uiInstance;

    private void Start(){
        // UI生成
        if (uiManagerPrefab != null){
            uiInstance = Instantiate(uiManagerPrefab);
            var uiManager = uiInstance.GetComponent<UIManager>();
            if (uiManager != null)
                GameManager.Instance.RegisterUI(uiManager);
        }

        // BGM再生
        if (bgmID >= 0)
            GameManager.Instance.PlayBGM(bgmID);
        else
            GameManager.Instance.PlayBGM(GameManager.Instance.CurrentStage);
    }

    private void OnDestroy(){
        if (GameManager.Instance != null)
            GameManager.Instance.UnregisterUI();
    }
}
