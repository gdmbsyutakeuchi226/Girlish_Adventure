/* =======================================
 * ファイル名 : Move_GroundPatrol.cs
 * 概要 : 地上移動型
 * Date : 2025/10/21
 * ======================================= */
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/MoveBehavior/GroundPatrol")]
public class Move_GroundPatrol : MoveBehaviorSO {
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float edgeCheckDistance = 0.5f;

    public override void Move(BaseEnemy enemy, MoveState state){
        Rigidbody2D rb = enemy.Rb;
        Vector2 dir = enemy.MoveDirection;

        rb.linearVelocity = new Vector2(dir.x * enemy.MoveSpeed, rb.linearVelocity.y);

        // 崖チェック
        Vector2 checkPos = (Vector2)enemy.transform.position + Vector2.right * dir.x * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, edgeCheckDistance, groundLayer);
        if (!hit){
            enemy.MoveDirection = -dir; // 反転
        }
    }
}
