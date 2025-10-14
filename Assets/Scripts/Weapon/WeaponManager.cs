using UnityEngine;

public class WeaponManager : MonoBehaviour {
    [Header("現在装備中の武器")]
    public WeaponBase currentWeapon;

    [Header("プレイヤーの向き")]
    public bool facingRight = true;

    public void Attack(Vector2 inputDir){
        if (currentWeapon == null) return;

        // 入力方向（右／左／上／斜め）を決定
        Vector2 dir = Vector2.zero;

        if (inputDir.y > 0.5f){
            if (Mathf.Abs(inputDir.x) > 0.5f)
                dir = new Vector2(Mathf.Sign(inputDir.x), 1f).normalized; // 斜め上
            else
                dir = Vector2.up; // 真上
        }else{
            dir = facingRight ? Vector2.right : Vector2.left;
        }

        currentWeapon.StartAttack(dir);
    }

    public void Flip(bool right){
        facingRight = right;
        if (currentWeapon != null){
            Vector3 scale = currentWeapon.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (right ? 1 : -1);
            currentWeapon.transform.localScale = scale;
        }
    }
}
