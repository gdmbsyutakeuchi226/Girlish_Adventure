using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private MonoBehaviour eventScript; // 会話後に実行したいスクリプト
    [SerializeField] private string eventMethodName = "OnDialogueEnd"; // 実行メソッド名

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other){
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        var window = FindObjectOfType<DialogueWindow>();
        if (window != null){
            window.StartDialogue(dialogueData, OnDialogueFinished);
        }
    }

    private void OnDialogueFinished(){
        if (eventScript != null){
            var method = eventScript.GetType().GetMethod(eventMethodName);
            if (method != null)
                method.Invoke(eventScript, null);
        }
    }
}
