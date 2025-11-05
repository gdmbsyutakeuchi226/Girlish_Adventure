/* =======================================
 * ファイル名 : DialogueData.cs
 * 概要 : 台詞データスクリプト
 * Create Date : 2025/11/05
 * Date : 2025/11/05
 * Version : 0.02
 * 更新内容 : キャラ名を入れられるようにしました。
 * Type : ScriptableObject
 * ======================================= */
using UnityEngine;

[System.Serializable]
public class DialogueLine {
    public string speakerName;   // キャラ名
    [TextArea(3, 5)]
    public string text;          // セリフ
}

[CreateAssetMenu(menuName = "Event/DialogueData")]
public class DialogueData : ScriptableObject {
    public DialogueLine[] lines;
}

/* ---------------------------------------
 * Canvas
 └ DialogueWindow (Imageなどで枠)
     ├ Text (TextMeshProUGUI)
     └ NextIcon (▼など、点滅アイコン) ※任意
 * ---------------------------------------
 * DialogueUI を DialogueWindow にアタッチ
 * Text に TextMeshProUGUI を設定
 * DialogueManager の ui に DialogueUI を指定
 * dialogueData に会話データSOを指定
 * ---------------------------------------
 * サンプルデータ例(DialogueData.asset)
 * pages:
  - "こんにちは、旅の人。\nここは古の神殿です。"
  - "この先には危険が待ち受けています。\n覚悟はできていますか？"
  - "……そうですか。\nあなたの勇気を信じましょう。"
 * --------------------------------------
 * 🎮 セットアップ手順
 * DialogueData をProjectに作成 → 登場キャラ名と台詞を入力
 * Canvas上に DialogueWindow prefab を置き、Text欄と名前欄を紐づけ
 * トリガーオブジェクトに DialogueTrigger を付けて
 * DialogueData
 * イベントスクリプト（例：DoorEvent）
 * イベントメソッド名（例："OnDialogueEnd"） を指定
*/