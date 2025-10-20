using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour{
    [Header("地面判定レイヤー")]
    public LayerMask groundLayer;
    [Header("接地判定用トランスフォーム")]
    public Transform checkPoint;
    [Header("判定半径")]
    public float checkRadius = 0.3f;

    public bool IsGrounded { get; private set; }
    public Vector2 GroundNormal { get; private set; } = Vector2.up; // 坂の法線向き
    // Ground状態が変化したときに通知するイベント
    public event Action<bool> OnGroundedChanged;

    private bool prevGrounded;
    private Collider2D currentGroundCollider;

    private void Update(){
        // 円判定
        Collider2D hit = Physics2D.OverlapCircle(checkPoint.position, checkRadius, groundLayer);
        IsGrounded = hit != null;

        if (hit != null){
            // 現在の接地オブジェクトを記録
            currentGroundCollider = hit;
            
            // 下方向へ短いRaycastで坂の法線を取得
            RaycastHit2D rayHit = Physics2D.Raycast(checkPoint.position, Vector2.down, 0.3f, groundLayer);
            if (rayHit)
                GroundNormal = rayHit.normal; // 坂の角度方向
            else
                GroundNormal = Vector2.up;
        }else{
            currentGroundCollider = null;
            GroundNormal = Vector2.up;
        }

        // 接地変化イベントを通知
        if (IsGrounded != prevGrounded){
            OnGroundedChanged?.Invoke(IsGrounded);
            prevGrounded = IsGrounded;
        }
    }

    // 足元の動く床の速度を取得（なければゼロ）
    public Vector2 GetGroundVelocity(){
        if (currentGroundCollider == null) return Vector2.zero;
        var moveObject = currentGroundCollider.GetComponent<MoveObject>();
        return moveObject != null ? moveObject.GetVelocity() : Vector2.zero;
    }

    void OnDrawGizmosSelected(){
        if (checkPoint == null) return;
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, checkRadius);
        Gizmos.DrawLine(checkPoint.position, checkPoint.position + (Vector3)(-GroundNormal * 0.5f));
    }
}
/* 旧処理
 *     void Update(){
        currentGroundCollider = Physics2D.OverlapCircle(checkPoint.position, checkRadius, groundLayer);
        IsGrounded = currentGroundCollider != null;

        if (currentGroundCollider != null)
            Debug.Log($"Ground hit: {currentGroundCollider.name}, Layer: {LayerMask.LayerToName(currentGroundCollider.gameObject.layer)}");

        // 前回と異なる場合にイベントを発火
        if (IsGrounded != prevGrounded){
            OnGroundedChanged?.Invoke(IsGrounded);
            prevGrounded = IsGrounded;
        }
    }
*/