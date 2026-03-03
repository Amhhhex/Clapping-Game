using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ArmMovement : MonoBehaviour
{

    public GameObject leftHand;

    public GameObject rightHand;


    public float handSpeed;

    public bool moveLeftHand;
    public bool returnLeftHand;
    public AnimationCurve aniCurveMove;

    public AnimationCurve aniCurveReturn;
    public float leftHandMoveTimer;
    public float leftHandReturnTimer;
    Vector3 leftReturnPosition;


    public bool moveRightHand;
    public bool returnRightHand;
    public float rightHandMoveTimer;
    public float rightHandReturnTimer;
    Vector3 rightReturnPosition;

    float handDistance;
    float score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 leftHandPosition = leftHand.transform.localPosition;

        Vector3 rightHandPosition = rightHand.transform.localPosition;


        


        if(Keyboard.current.kKey.isPressed && !moveLeftHand && !returnLeftHand)
        {

            leftHandPosition.y += handSpeed * Time.deltaTime;


        }

        if(Keyboard.current.jKey.isPressed && !moveLeftHand && !returnLeftHand)
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


        if(Mouse.current.leftButton.wasReleasedThisFrame && !moveLeftHand)
        {
            moveLeftHand = true;
            leftHandMoveTimer = 0f;

            leftReturnPosition = leftHand.transform.localPosition;
        }

       
        if(moveLeftHand)
        {
            leftHandMoveTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveMove.Evaluate(leftHandMoveTimer);

            leftHandPosition = Vector3.Lerp(leftHandPosition, new Vector3(-1.3f, leftHandPosition.y, leftHandPosition.z), aniCurveTimer);
        }

        if (leftHandMoveTimer >= 1f)
        {

            returnLeftHand = true;
            moveLeftHand = false;

            leftHandMoveTimer = 0f;
        }

        if(returnLeftHand)
        {
            leftHandReturnTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveReturn.Evaluate(leftHandReturnTimer);

            leftHandPosition = Vector3.Lerp(leftHandPosition, leftReturnPosition, aniCurveTimer);

        }

        if(leftHandPosition == leftReturnPosition && returnLeftHand)
        {
            returnLeftHand = false;
            leftHandReturnTimer = 0f;
        }

        //Right hand movement

        if (Mouse.current.rightButton.wasReleasedThisFrame && !moveRightHand)
        {
            moveRightHand = true;
            rightHandMoveTimer = 0f;

            rightReturnPosition = rightHand.transform.localPosition;
        }


        if (moveRightHand)
        {
            rightHandMoveTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveMove.Evaluate(rightHandMoveTimer);

            rightHandPosition = Vector3.Lerp(rightHandPosition, new Vector3(-1.2f, rightHandPosition.y, rightHandPosition.z), aniCurveTimer);
        }

        if (rightHandMoveTimer >= 1f)
        {

            returnRightHand = true;
            moveRightHand = false;

            handDistance = Vector3.Distance(rightHandPosition, leftHandPosition);


            score = math.remap(0, 1, 50, 0, handDistance);
            

            rightHandMoveTimer = 0f;
        }

        if (returnRightHand)
        {
            rightHandReturnTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveReturn.Evaluate(rightHandReturnTimer);

            rightHandPosition = Vector3.Lerp(rightHandPosition, rightReturnPosition, aniCurveTimer);

        }

        if (rightHandPosition == rightReturnPosition && returnRightHand)
        {
            returnRightHand = false;
            rightHandReturnTimer = 0f;
        }




        print(score);






        leftHandPosition.y = Mathf.Clamp(leftHandPosition.y, -0.5f, 0.222f);

        rightHandPosition.y = Mathf.Clamp(rightHandPosition.y, -0.5f, 0.222f);

        leftHand.transform.localPosition = leftHandPosition;
        rightHand.transform.localPosition = rightHandPosition;



        
    }
}
