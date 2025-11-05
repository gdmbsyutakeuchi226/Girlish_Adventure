/* =======================================
 * ファイル名 : DialogueData.cs
 * 概要 : 台詞データスクリプト
 * Create Date : 2025/11/05
 * Date : 2025/11/05
 * Version : 0.01
 * 更新内容 : 新規作成
 * Type : ScriptableObject
 * ======================================= */
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject {
    [TextArea(3, 5)]
    public string[] pages; // 各ページの文章（最大3行程度を推奨）
}

