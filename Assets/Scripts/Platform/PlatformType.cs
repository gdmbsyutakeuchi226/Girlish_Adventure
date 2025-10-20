using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class PlatformType : MonoBehaviour {
    [Tooltip("プレイヤーが↓+ジャンプで床を落下できるならON。falseで下キー通過不可（ただし一方通行は有効）")]
    public bool allowDropThrough = false;

    // エディタから切り替えたいだけなら Awakeで設定する必要は無いが、
    // ここでEffectorの初期設定を行うことも可能。
    void Awake(){
        var eff = GetComponent<PlatformEffector2D>();
        if (eff != null){
            // 通常はOneWayを有効にしておく（上からのみ接触）
            eff.useOneWay = true;
            eff.surfaceArc = 160f; // あなたが調整済みの値
        }
    }
}
