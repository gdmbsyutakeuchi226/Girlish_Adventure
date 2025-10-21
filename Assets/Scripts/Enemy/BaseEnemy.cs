/* =======================================
 * スクリプト名：BaseEnemy.cs
 * 概要 : 敵スクリプトの基底クラス
 * Date : 2025/10/21
 * Version : 0.03
 * 更新内容 : ScriptableObjectに換装
 * ======================================= */
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {
    [Header("共通パラメータ")]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected int maxHP = 10;
    [SerializeField] protected int attackPower = 1;
    [SerializeField] protected MoveBehaviorSO moveBehavior;

    protected int currentHP;
    protected Rigidbody2D rb;
    protected Vector2 moveDirection = Vector2.left;

    // 各敵固有の動的状態を保持
    protected MoveState moveState = new MoveState();

    public Transform Player { get; set; }
    public Rigidbody2D Rb => rb;
    public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public float MoveSpeed => moveSpeed;

    protected virtual void Start(){
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;

        // MoveState 初期化（個別データ生成）
        moveState = new MoveState();
        moveBehavior?.Initialize(this, moveState);
    }

    protected virtual void FixedUpdate(){
        moveBehavior?.Move(this, moveState);
    }

    public virtual void TakeDamage(int amount){
        currentHP -= amount;
        if (currentHP <= 0) Die();
    }

    protected virtual void Die(){
        Destroy(gameObject);
    }

    public abstract void Attack(PlayerController player);
}