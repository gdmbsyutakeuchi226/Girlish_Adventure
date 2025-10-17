using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour{
    [Header("地面判定レイヤー")]
    [SerializeField] private LayerMask groundLayer;
    [Header("接地判定用トランスフォーム")]
    [SerializeField] private Transform checkPoint;
    [Header("判定半径")]
    [SerializeField] private float checkRadius = 0.1f;

    public bool IsGrounded { get; private set; }
    // Ground状態が変化したときに通知するイベント
    public event Action<bool> OnGroundedChanged;

    private bool prevGrounded;
    private GameObject hit;

    void Update(){
        Collider2D hit = Physics2D.OverlapCircle(checkPoint.position, checkRadius, groundLayer);
        IsGrounded = hit != null;

        if (hit != null)
            Debug.Log($"Ground hit: {hit.name}, Layer: {LayerMask.LayerToName(hit.gameObject.layer)}");

        // 前回と異なる場合にイベントを発火
        if (IsGrounded != prevGrounded){
            OnGroundedChanged?.Invoke(IsGrounded);
            prevGrounded = IsGrounded;
        }
    }

    void OnDrawGizmosSelected(){
        if (checkPoint == null) return;
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, checkRadius);
    }
}
