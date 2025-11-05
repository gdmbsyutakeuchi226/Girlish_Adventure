using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {
    [Header("ゴールファンファーレ（BGM扱い）")]
    [SerializeField] private int goalFanfareID = 99; // bgmDBに登録済みのID
    [Header("リザルトシーン名")]
    [SerializeField] private string resultSceneName = "Result";

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other){
        if (isTriggered) return;
        if (!other.CompareTag("Player")) return;

        isTriggered = true;
        StartCoroutine(GoalSequence());
    }

    private IEnumerator GoalSequence(){
        PauseManager.Instance.SetPause(true); // ← 一時停止

        GameManager.Instance.StopBGM();
        GameManager.Instance.PlayBGM(goalFanfareID);

        yield return new WaitForSeconds(3.5f);

        // リザルト画面へ
        GameManager.Instance.GoResult(resultSceneName);
    }
}
