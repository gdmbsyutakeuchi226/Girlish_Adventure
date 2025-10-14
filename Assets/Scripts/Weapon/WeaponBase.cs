/* =======================================
 * �X�N���v�g���FWeaponBase.cs
 * ����̊��N���X
 * =======================================
 */
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour{
    [Header("���ʐݒ�")]
    public int damage = 1;
    public float attackDuration = 0.3f;
    public Vector2 attackDirection = Vector2.right;

    protected bool isAttacking = false;
    protected Collider2D hitbox;

    protected virtual void Awake(){
        hitbox = GetComponent<Collider2D>();
        if(hitbox != null) hitbox.enabled = false;
    }

    public virtual void StartAttack(Vector2 dir){
        if(isAttacking) return;
        attackDirection = dir;
        isAttacking = true;
        hitbox.enabled = true;
        OnAttackStart();
        Invoke(nameof(EndAttack), attackDuration);
    }
    // �U�����̋����i�e����ŏ㏑���j
    protected abstract void OnAttackStart();

    public virtual void EndAttack(){
        isAttacking = false;
        if (hitbox != null){
            hitbox.enabled = false;
        }
        OnAttackEnd();
    }
    protected virtual void OnAttackEnd() { }

    protected virtual void OnTriggerEnter2D(Collider2D other){
        if (!isAttacking) return;

        if (other.CompareTag("Enemy")){
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            if (enemy != null){
                enemy.TakeDamage(damage);
                Debug.Log($"{name} �� {enemy.name} �� {damage} �_���[�W!");
            }
        }
    }
}
