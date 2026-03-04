using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    public Transform playerObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerObj);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
