/* =======================================
 * ファイル名 : DialogueManager.cs
 * 概要 : 実行・ページ制御スクリプト
 * Create Date : 2025/11/05
 * Date : 2025/11/05
 * Version : 0.02
 * 更新内容 : タイプ音付きバージョン
 * ======================================= */
using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private DialogueUI ui;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private float typeSpeed = 0.03f; // 文字の出る速度
    [SerializeField] private int typeSoundId = 5; // ← タイプ音のSE ID（任意番号）

    private int currentPage = 0;
    private bool isTyping = false;
    private bool waitingForNext = false;
    private GameManager gameManager; // ← 追加

    private void Start(){
        gameManager = GameManager.Instance; // 例：シングルトン想定
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue(){
        while (currentPage < dialogueData.pages.Length){
            yield return StartCoroutine(TypePage(dialogueData.pages[currentPage]));

            waitingForNext = true;
            yield return new WaitUntil(() => Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Space));
            waitingForNext = false;

            currentPage++;
        }

        ui.HideWindow();
    }

    private IEnumerator TypePage(string text){
        isTyping = true;
        ui.ClearText();

        foreach (char c in text){
            ui.AppendText(c.ToString());


            // --- ここでタイプ音を再生 ---
            if (c != ' ' && c != '\n'){
                // 空白・改行は無音
                gameManager.PlaySE(typeSoundId);
            }
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }
}
