using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ArmMovement : MonoBehaviour
{
    public InputActionReference ClappingP1Input;
    public InputActionReference ClappingP2Input;
    public InputActionReference HandMovementP1Input;
    public InputActionReference HandMovementP2Input;

    //The left and right hand game objects
    public GameObject leftHand;
    public GameObject rightHand;

    //The speed at which the hands move in the Y-Axis
    public float handSpeed;

    //The animation curves for moving in and returning back out
    public AnimationCurve aniCurveMove;
    public AnimationCurve aniCurveReturn;

    //State switching between moving the left hand and returning the left hand
    public bool moveLeftHand;
    public bool returnLeftHand;
    
    //The timers for interprolating the hand to the center and to the return position
    public float leftHandMoveTimer;
    public float leftHandReturnTimer;
    Vector3 leftReturnPosition;

    //Same as above except for the right hand
    public bool moveRightHand;
    public bool returnRightHand;
    public float rightHandMoveTimer;
    public float rightHandReturnTimer;
    Vector3 rightReturnPosition;

    //A global variable for distance and score measurement
    float handDistance;
    float score;

    //seperate variables for left and right hand powers
    public float rightHandPower;
    public float leftHandPower;

    //the amount in which the hand powers will increase
    public float powerIncrease;

    //State change for checking claps
    public bool clapCheck;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Store the position of the left and right hands in local space
        Vector3 leftHandPosition = leftHand.transform.localPosition;

        Vector3 rightHandPosition = rightHand.transform.localPosition;

        Debug.Log(HandMovementP1Input.action.ReadValue<Vector2>());
        

        //Move the left and right hand's up or down based on which keys are held
        if(HandMovementP1Input.action.ReadValue<Vector2>().y == 1 && !moveLeftHand && !returnLeftHand)
        {

            leftHandPosition.y += handSpeed * Time.deltaTime;


        }

        if(HandMovementP1Input.action.ReadValue<Vector2>().y == -1 && !moveLeftHand && !returnLeftHand)
        {

            leftHandPosition.y -= handSpeed * Time.deltaTime;

        }

        if(HandMovementP2Input.action.ReadValue<Vector2>().y == 1 && !moveRightHand && !returnRightHand)
        {
            rightHandPosition.y += handSpeed * Time.deltaTime;
        }

        if(HandMovementP2Input.action.ReadValue<Vector2>().y == -1 && !moveRightHand && !returnRightHand)
        {
            rightHandPosition.y -= handSpeed * Time.deltaTime;
        }


        //While the left button is held down increase the leftHandPower var
        if(ClappingP1Input.action.IsPressed() && !moveLeftHand)
        {
            leftHandPower += powerIncrease * Time.deltaTime;

            if(leftHandPower >= 25f)
            {
                leftHandPower = 25f;
            }
        }
         
        //Once the left button is realeased start movement towards the center, disable movement of the hands while in movtion
        if(ClappingP1Input.action.WasReleasedThisFrame() && !moveLeftHand)
        {
            moveLeftHand = true;
            leftHandMoveTimer = 0f;

            leftReturnPosition = leftHand.transform.localPosition;
        }

       //Interprolating the movement of the left hand towards the center of the screen
        if(moveLeftHand)
        {
            leftHandMoveTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveMove.Evaluate(leftHandMoveTimer);

            leftHandPosition = Vector3.Lerp(leftHandPosition, new Vector3(-1.19f, leftHandPosition.y, leftHandPosition.z), aniCurveTimer);
        }

        //Once the hand reaches the middle of the screen, toggle the move to false and the return state to true
        //also reset the timer
        if (leftHandMoveTimer >= 1f)
        {

            returnLeftHand = true;
            moveLeftHand = false;

            leftHandMoveTimer = 0f;
        }

        //Interp the left hand from the center to its return position
        if(returnLeftHand)
        {
            leftHandReturnTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveReturn.Evaluate(leftHandReturnTimer);

            leftHandPosition = Vector3.Lerp(leftHandPosition, leftReturnPosition, aniCurveTimer);

        }

        //if the left hand is in the return position, set the timer and power to zero and give control back
        if(leftHandPosition == leftReturnPosition && returnLeftHand)
        {
            returnLeftHand = false;
            leftHandReturnTimer = 0f;
            leftHandPower = 0f;
        }

        //Right hand movement
        //Everything that applied to left hand movement is the exact same here, instead just for the right hand

        if(ClappingP2Input.action.IsPressed() && !moveRightHand)
        {
            rightHandPower += powerIncrease * Time.deltaTime;

            if(rightHandPower >= 25f)
            {
                rightHandPower = 25f;
            }
        }

        if (ClappingP2Input.action.WasReleasedThisFrame() && !moveRightHand)
        {
            moveRightHand = true;
            rightHandMoveTimer = 0f;

            rightReturnPosition = rightHand.transform.localPosition;
        }


        if (moveRightHand)
        {
            rightHandMoveTimer += Time.deltaTime;

            float aniCurveTimer = aniCurveMove.Evaluate(rightHandMoveTimer);

            rightHandPosition = Vector3.Lerp(rightHandPosition, new Vector3(-1.23f, rightHandPosition.y, rightHandPosition.z), aniCurveTimer);
        }

        



        if (handDistance <= 0.5f)
        {
            print(handDistance);
            
        }



        if (rightHandMoveTimer >= 1f)
        {

            returnRightHand = true;
            moveRightHand = false;

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

            rightHandPower = 0f;
        }








        //print("Absolute Value: " + math.abs(rightHandPosition.x - leftHandPosition.x));


        //Once both hands are in the center of the screen, check if a clap was done
        if (math.abs(rightHandPosition.x - leftHandPosition.x) < 0.1f)
        {
            print("CLAP!!!!!!!!!!");
            clapCheck = true;
        }

        else
        {
            clapCheck = false;
        }

        //during a clap check, grab the Vector3 distance between the two hands
        //remap that value to 50 - 0, 50 being the closest they could possibly be

        //Then add the powers of the other two hands to the score
        if (clapCheck)
        {
            handDistance = Vector3.Distance(rightHandPosition, leftHandPosition);


            score = math.remap(0, 1, 50, 0, handDistance);

            score += leftHandPower;
            score += rightHandPower;

        }

        /*
        print("Distance between the hands: " + handDistance);

        print(score);

        */

        //
        
        //Assigning a value with the various score totals
        if (score >= 90f)
        {
            print("PERFECT!!");
        }
        else if (score >= 80f)
        {
            print("Great!");
        }

        else if(score >= 50f)
        {
            print("CMON MAN");
        }

        else if (score <= 50f)
        {
            print("pathetic");
        }

        

        //restrict the left and right hand Y positions
        leftHandPosition.y = Mathf.Clamp(leftHandPosition.y, -0.5f, 0.6f);

        rightHandPosition.y = Mathf.Clamp(rightHandPosition.y, -0.5f, 0.6f);



        //Change the transforms of the left and right hand to their newly update spots
        leftHand.transform.localPosition = leftHandPosition;
        rightHand.transform.localPosition = rightHandPosition;



        
    }
}
