/* =======================================
 * ファイル名 : DialogueManager.cs
 * 概要 : 実行・ページ制御スクリプト
 * Create Date : 2025/11/05
 * Date : 2025/11/05
 * Version : 0.01
 * 更新内容 : 新規作成
 * ======================================= */
using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private DialogueUI ui;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private float typeSpeed = 0.03f; // 文字の出る速度

    private int currentPage = 0;
    private bool isTyping = false;
    private bool waitingForNext = false;

    private void Start(){
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
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }
}
