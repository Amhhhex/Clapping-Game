using UnityEngine;
using UnityEngine.InputSystem;

public class ArmMovement : MonoBehaviour
{

    public GameObject leftHand;

    public GameObject rightHand;


    public float handSpeed;


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



       


        leftHandPosition.y = Mathf.Clamp(leftHandPosition.y, -0.5f, 0.222f);

        rightHandPosition.y = Mathf.Clamp(rightHandPosition.y, -0.5f, 0.222f);

        print(leftHandPosition.y);

        print("hello");

        leftHand.transform.localPosition = leftHandPosition;
        rightHand.transform.localPosition = rightHandPosition;



        
    }
}
