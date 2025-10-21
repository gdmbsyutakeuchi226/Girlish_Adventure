/* =======================================
 * ファイル名 : IMoveBehavior.cs
 * 概要 : 移動用ScriptableObject
 * Date : 2025/10/21
 * ======================================= */
using UnityEngine;

public abstract class MoveBehaviorSO : ScriptableObject {
    /// <summary>
    /// 移動パターンごとの初期化。敵生成時に一度呼ばれる。
    /// </summary>
    public virtual void Initialize(BaseEnemy enemy, MoveState state) { }

    /// <summary>
    /// 毎FixedUpdateで呼ばれる移動処理。
    /// </summary>
    public abstract void Move(BaseEnemy enemy, MoveState state);
}
