using UnityEngine;

public class SwordWeapon : WeaponBase {
    [Header("�U���������Ƃ̃��[�J����]")]
    public float upAngle = 0f;
    public float diagonalAngle = 45f;
    public float forwardAngle = 90f;

    protected override void OnAttackStart(){
        // �U�������ɉ����ĉ�]�p�x��ݒ�
        float angle = forwardAngle;

        if (attackDirection == Vector2.up){
            angle = upAngle;
        }else if (attackDirection == new Vector2(1, 1).normalized || attackDirection == new Vector2(-1, 1).normalized){
            angle = diagonalAngle;
        }
        transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(attackDirection.x == 0 ? 1 : attackDirection.x));
    }

    protected override void OnAttackEnd(){
        transform.localRotation = Quaternion.identity;
    }
}
