using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    float rotationSpeed = 25f;
    Vector2 CameraDirection;
    public ArmMovement armMovementScr;
    public Rigidbody PlayerBody;
    public Camera PlayerCamera;
    public InputActionReference CameraP1Input;
    public InputActionReference CameraP2Input;
    public InputActionReference ClappingP1Input;
    public InputActionReference ClappingP2Input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //armMovementScr = GetComponent<ArmMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (armMovementScr.clapCheck == true)
        {
            //Add force by clap score divided by maximum
            PlayerBody.AddForce(transform.forward, ForceMode.Impulse);
            armMovementScr.clapCheck = false;
        }
    }

    void CameraMovement()
    {
        CameraDirection = new Vector2(-CameraP1Input.action.ReadValue<Vector2>().y + -CameraP2Input.action.ReadValue<Vector2>().y, CameraP1Input.action.ReadValue<Vector2>().x + CameraP2Input.action.ReadValue<Vector2>().x);
        transform.eulerAngles = PlayerCamera.transform.eulerAngles + (Vector3)CameraDirection * rotationSpeed * Time.deltaTime;
    }
}
