using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class PlatformType : MonoBehaviour {
    [Tooltip("�v���C���[����+�W�����v�ŏ��𗎉��ł���Ȃ�ON�Bfalse�ŉ��L�[�ʉߕs�i����������ʍs�͗L���j")]
    public bool allowDropThrough = false;

    // �G�f�B�^����؂�ւ����������Ȃ� Awake�Őݒ肷��K�v�͖������A
    // ������Effector�̏����ݒ���s�����Ƃ��\�B
    void Awake(){
        var eff = GetComponent<PlatformEffector2D>();
        if (eff != null){
            // �ʏ��OneWay��L���ɂ��Ă����i�ォ��̂ݐڐG�j
            eff.useOneWay = true;
            eff.surfaceArc = 160f; // ���Ȃ��������ς݂̒l
        }
    }
}
