using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour{
    [SerializeField, Header("�U�����鎞��")]
    private float shakeTime;
    [SerializeField, Header("�U���̑傫��")]
    private float shakeMagnitude;

    [SerializeField] private PlayerController player;

    private Vector3 initpos;
    private float shakeCount;
    private int currentPlayerHP;

    void Start(){
        //player = FindObjectOfType<PlayerController>();
        currentPlayerHP = player.GetHP();
        initpos = transform.position;
    }
    private void Update(){
        ShakeCheck();
        FollowPlayer();
    }
    // HP������ƐU��������(�����I��DoTween�Œ���)
    private void ShakeCheck(){
        if(currentPlayerHP != player.GetHP()){
            currentPlayerHP = player.GetHP();
            shakeCount = 0.0f;
            StartCoroutine(Shake());
        }
    }
    // �U������(�����I��DoTween�Œ���)
    IEnumerator Shake(){
        Vector3 initpos = transform.position;
        while(shakeCount < shakeTime){
            float x = initpos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = initpos.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = new Vector3(x, y, initpos.z);

            shakeCount += Time.deltaTime;
            yield return null;
        }
        transform.position = initpos;
    }
    //�J�����Ǐ](�����I��ChinemachineCamera�Œ���)
    private void FollowPlayer(){
        float x = player.transform.position.x;
        x = Mathf.Clamp(x, initpos.x, Mathf.Infinity);
        transform.position = new Vector3(x,transform.position.y, transform.position.z); 
    }

}
