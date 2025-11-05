using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class DialogueWindow : MonoBehaviour {
    [Header("UI参照")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject windowRoot;
    [SerializeField] private float typeSpeed = 0.03f;

    private DialogueData currentData;
    private int currentIndex = 0;
    private bool isTyping = false;
    private bool isWaiting = false;
    private Action onDialogueEnd; // 終了後のコールバック

    private GameManager gameManager => GameManager.Instance;

    private void Start()
    {
        windowRoot.SetActive(false);
    }

    public void StartDialogue(DialogueData data, Action onEnd = null)
    {
        if (data == null) return;

        // 会話開始時はゲームを一時停止
        gameManager.SetGamePaused(true);

        currentData = data;
        currentIndex = 0;
        onDialogueEnd = onEnd;
        windowRoot.SetActive(true);

        ShowNextLine();
    }

    private void Update()
    {
        if (!windowRoot.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Submit"))
        {
            if (isTyping)
            {
                // タイプ中ならスキップして全文表示
                StopAllCoroutines();
                dialogueText.text = currentData.lines[currentIndex].text;
                isTyping = false;
                isWaiting = true;
            }
            else if (isWaiting)
            {
                // ページ送り
                currentIndex++;
                ShowNextLine();
            }
        }
    }

    private void ShowNextLine()
    {
        if (currentData == null || currentIndex >= currentData.lines.Length)
        {
            EndDialogue();
            return;
        }

        var line = currentData.lines[currentIndex];
        nameText.text = line.speakerName;
        StartCoroutine(TypeText(line.text));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        isWaiting = false;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            if (c != ' ' && c != '\n')
            {
                gameManager.PlaySE(3); // タイプ音ID例
            }
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        isWaiting = true;
    }

    private void EndDialogue()
    {
        windowRoot.SetActive(false);
        currentData = null;
        onDialogueEnd?.Invoke(); // イベントコール
        gameManager.SetGamePaused(false); // ゲーム再開
    }
}
