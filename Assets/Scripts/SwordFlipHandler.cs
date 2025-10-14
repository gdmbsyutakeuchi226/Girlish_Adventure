using UnityEngine;

public class SwordFlipHandler : MonoBehaviour{
    [SerializeField] private Transform swordTransform;
    [SerializeField] private Vector3 rightLocalPosition = new Vector3(0.5f, 0f, 0f);
    [SerializeField] private Vector3 leftLocalPosition = new Vector3(-0.5f, 0f, 0f);

    public void UpdateSwordDirection(bool facingRight){
        if(swordTransform != null) return; 
        
        if(facingRight){
            swordTransform.localPosition = rightLocalPosition;
            swordTransform.localScale = new Vector3(1, 1, 1);
        }else{
            swordTransform.localPosition = leftLocalPosition;
            swordTransform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
