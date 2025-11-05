/* =======================================
 * ファイル名 : GameManager.cs
 * 概要 : ゲームメインスクリプト
 * Create Date : 2025/10/01
 * Date : 2025/10/24
 * Version : 0.04
 * 更新内容 : Presistent対応
 * ======================================= */
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private UIManager uiManager;
    public int CurrentStage { get; private set; }

    [SerializeField] private SoundManager soundManager; // InspectorでPersistent上のSoundManager参照
    public SoundManager SoundManager { get; private set; }
    public UIManager UI => uiManager;

    private bool persistentLoaded = false;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(EnsurePersistentLoaded());
        }else{
            Destroy(gameObject);
        }
    }

    private IEnumerator EnsurePersistentLoaded(){
        // PersistentがロードされていなければAdditiveロード
        if (!SceneManager.GetSceneByName("Persistent").isLoaded){
            Debug.Log("Persistentシーンが未ロードのため、強制ロードを実行します。");
            var async = SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);
            while (!async.isDone) yield return null;
        }

        // SoundManagerが現れるまで探す
        float timeout = 5f;
        while (soundManager == null && timeout > 0f){
            soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null){
                Debug.Log($"✅ SoundManagerを検出しました -> {soundManager.name}");
                yield break;
            }
            timeout -= Time.unscaledDeltaTime;
            yield return null;
        }

        if (soundManager == null)
            Debug.LogError("❌ SoundManagerが見つかりません。Persistentシーンに正しく配置されているか確認してください。");
    }

    public void SetGamePaused(bool isPaused){
        // タイムスケールを止めて物理・アニメを停止
        Time.timeScale = isPaused ? 0f : 1f;

        // プレイヤー操作を禁止したい場合
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.enabled = !isPaused;

        Debug.Log($"Game Paused: {isPaused}");
    }

    // ===== UI関連 =====

    public void RegisterUI(UIManager ui){
        uiManager = ui;
    }

    public void UnregisterUI(){
        uiManager = null;
    }

    // ===== シーン遷移 =====

    public void LoadStage(int stageNumber){
        CurrentStage = stageNumber;
        SceneManager.LoadSceneAsync($"Stage_{stageNumber}");
    }

    public void ReturnToWorldMap(){
        SceneManager.LoadSceneAsync("WorldMap");
    }

    public void GoToTitle(){
        SceneManager.LoadSceneAsync("Title");
    }
    public void GoResult(string resultSceneName){
        SceneManager.LoadSceneAsync(resultSceneName);
    }



    // ===== サウンド操作 =====


    public void PlayBGM(int bgmID){
        if (soundManager == null){
            soundManager = FindObjectOfType<SoundManager>();
        }
        if (soundManager != null){
            soundManager.PlayBGM(bgmID);
        }else{
            Debug.LogWarning("⚠️ SoundManager未設定。Persistentロードを再試行。");
            StartCoroutine(EnsurePersistentLoaded());
        }
    }

    public void PlaySE(int seID){
        if (soundManager == null)
            soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
            soundManager.PlaySE(seID);
    }

    public void StopBGM(){
        soundManager?.StopBGM();
    }

}
