/* ============================================================
 * �X�N���v�g���FBaseEnemy.cs
 * �G�X�N���v�g�̊��N���X
 * ============================================================
 */
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour{ 
    [Header("���ʃp�����[�^")]
    public float moveSpeed;
    public int maxHP;
    public int attackPower;

    protected int currentHP;
    protected Rigidbody2D rb;
    protected Vector2 moveDirection;

    public EnemyData enemyData;
    protected virtual void Start(){
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.left;

        if (enemyData != null){
            moveSpeed = enemyData.moveSpeed;
            maxHP = enemyData.maxHP;
            attackPower = enemyData.attackPower;
        }
        currentHP = maxHP;
    }
    protected virtual void FixedUpdate(){
        Move();
    }
    protected virtual void Move(){
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
    }
    public virtual void TakeDamage(int amonut){
        currentHP -= amonut;
        if(currentHP < 0){
            Die(); // HP��0�ɂȂ����ꍇ�͓G������
        }
    }
    protected virtual void Die(){
        Destroy(gameObject);
    }
    public abstract void Attack(PlayerController player);
}
