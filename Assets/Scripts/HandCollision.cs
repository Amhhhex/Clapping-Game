using UnityEngine;

public class HandCollision : MonoBehaviour
{
    public GameObject handCollidedObj;
    private void OnTriggerEnter(UnityEngine.Collider collider)
    {
        handCollidedObj = collider.gameObject;
    }
}
