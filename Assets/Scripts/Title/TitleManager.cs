/* =======================================
 * ファイル名 : TitleManager.cs
 * 概要 : タイトル画面スクリプト
 * Date : 2025/10/24
 * Version : 0.01
 * ======================================= */
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour{
    [Header("メニューパネル")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Button firstMenuButton;
    [SerializeField] private InputActionReference anyKeyAction; // Input Actionsから参照

    private bool menuOpened;

    private void OnEnable(){
        anyKeyAction.action.performed += OnAnyKey;
        anyKeyAction.action.Enable();
    }

    private void OnDisable(){
        anyKeyAction.action.performed -= OnAnyKey;
        anyKeyAction.action.Disable();
    }

    private void Start(){
        menuOpened = false;
        menuPanel.SetActive(false);
        gameStartPanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    private void OnAnyKey(InputAction.CallbackContext context){
        if (menuOpened) return;
        OpenMenu();
    }

    private void OpenMenu(){
        menuOpened = true;
        mainPanel.SetActive(false);
        menuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstMenuButton.gameObject);
    }

    public void OnGameStartSelected(){
        menuPanel.SetActive(false);
        gameStartPanel.SetActive(true);
    }

    public void OnOptionSelected(){
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void OnBackToMenu(){
        gameStartPanel.SetActive(false);
        optionPanel.SetActive(false);
        menuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstMenuButton.gameObject);
    }
}
/*--------------------------------
 * Button 設定
 * GameStartButton → OnClick() に TitleMenuController.OnGameStartSelected()
 * OptionButton → TitleMenuController.OnOptionSelected()
 * Back ボタンがある場合 → OnBackToMenu()
 * 
 * コントローラー対応
 * Input System UI Input Module はデフォルトで
 * 十字キー / スティック → 選択移動
 * A / Enter → 決定
 * B / Escape → 戻る
 * をサポート。
 * ポイント
 * 
 * EventSystem.current.SetSelectedGameObject() を使うことで、
 * コントローラー操作時も 初期選択ボタン が有効になる。
 * Unity 6 の UGUI Shader Graph Support により、UI 表示は軽量で美しく。
 ----------------------------------*/