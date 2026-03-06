using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    int rotationSpeed = 30;
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
        if (armMovementScr.clapCheck == true && armMovementScr.clapScore > 35f)
        {
            PlayerBody.AddForce(PlayerCamera.transform.forward * armMovementScr.clapScore, ForceMode.Impulse);
        }
    }

    void CameraMovement()
    {
        CameraDirection = new Vector2(-CameraP1Input.action.ReadValue<Vector2>().y + -CameraP2Input.action.ReadValue<Vector2>().y, CameraP1Input.action.ReadValue<Vector2>().x + CameraP2Input.action.ReadValue<Vector2>().x);
        PlayerCamera.transform.eulerAngles = PlayerCamera.transform.eulerAngles + (Vector3)CameraDirection * rotationSpeed * Time.deltaTime;
    }
}
