using UnityEngine;

public class BreakObject : MonoBehaviour{
    public GameObject breakObj;


    private void Start(){
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("PlayerAttack")){
            Destroy(breakObj);
        }
    }

}
