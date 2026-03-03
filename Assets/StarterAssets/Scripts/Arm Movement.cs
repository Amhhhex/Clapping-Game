using UnityEngine;
using UnityEngine.InputSystem;

public class ArmMovement : MonoBehaviour
{

    public GameObject leftHand;

    public GameObject rightHand;


    public float handSpeed;

    public bool moveLeftHand;
    public AnimationCurve aniCurve;
    public float leftHandTimer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 leftHandPosition = leftHand.transform.localPosition;

        Vector3 rightHandPosition = rightHand.transform.localPosition;




        if(Keyboard.current.kKey.isPressed)
        {

            leftHandPosition.y += handSpeed * Time.deltaTime;


        }

        if(Keyboard.current.jKey.isPressed)
        {

            leftHandPosition.y -= handSpeed * Time.deltaTime;

        }

        if(Keyboard.current.mKey.isPressed)
        {
            rightHandPosition.y += handSpeed * Time.deltaTime;
        }

        if(Keyboard.current.nKey.isPressed)
        {
            rightHandPosition.y -= handSpeed * Time.deltaTime;
        }


        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            moveLeftHand = true;
            leftHandTimer = 0f;
        }

       
        if(moveLeftHand)
        {
            leftHandTimer += Time.deltaTime;

            leftHandPosition = Vector3.Lerp(leftHandPosition, new Vector3(-1.5f, leftHandPosition.y, leftHandPosition.z), leftHandTimer);
        }


        leftHandPosition.y = Mathf.Clamp(leftHandPosition.y, -0.5f, 0.222f);

        rightHandPosition.y = Mathf.Clamp(rightHandPosition.y, -0.5f, 0.222f);

        print(leftHandPosition.y);

        leftHand.transform.localPosition = leftHandPosition;
        rightHand.transform.localPosition = rightHandPosition;



        
    }
}
