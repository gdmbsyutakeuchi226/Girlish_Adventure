using UnityEngine;

public class MapCameraFollow : MonoBehaviour{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate(){
        if (target == null) return;
        Vector3 targetPos = new(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
