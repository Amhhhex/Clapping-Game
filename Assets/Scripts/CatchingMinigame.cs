using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatchingMinigame : MonoBehaviour
{

    public GameObject playerObj;
    public GameObject clappingPosition;

    Rigidbody playerBody;


    public GameObject spawningPositionObject;

    public HandCollision leftHandCollidedScr;
    public HandCollision rightHandCollidedScr;


    ArmMovement armMovementScript;
    CharacterMovement characterMovementScript;

    bool catchingStarted;

    public float catchingTimer;

    public List<GameObject> fallingObjects = new List<GameObject>();

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    GameObject currentObject;

    Rigidbody objectsBody;

    Vector3 currentObjectTransform;

    Vector3 playerObjTransform;

    public float totalObjectsCaught;

    int listPosition;

    int score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armMovementScript = playerObj.GetComponentInChildren<ArmMovement>();
        characterMovementScript = playerObj.GetComponent<CharacterMovement>();

        playerBody = playerObj.GetComponent<Rigidbody>();

        playerObjTransform = playerObj.transform.position;

        totalObjectsCaught = 0;

        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(armMovementScript.returnLeftHand)
        {
            leftHandCollidedScr.handCollidedObj = null;
        }

        if(armMovementScript.returnRightHand)
        {
            rightHandCollidedScr.handCollidedObj = null;
        }

        if(catchingStarted)
        {

            catchingTimer += Time.deltaTime;

            timerText.text = catchingTimer.ToString();

            if (currentObject == null)
            {

                currentObject = Instantiate(fallingObjects[listPosition]);

                objectsBody = currentObject.GetComponent<Rigidbody>();

                currentObjectTransform = currentObject.transform.position;

                Vector3 spawningPositionTransform = spawningPositionObject.transform.position;

                objectsBody.MovePosition(spawningPositionTransform);

                listPosition++;
            }

            if(listPosition > fallingObjects.Count -1)
            {
                listPosition = 0;
            }

            if(leftHandCollidedScr.handCollidedObj == currentObject && rightHandCollidedScr.handCollidedObj == currentObject)
            {
                totalObjectsCaught++;


                if(currentObject.name == "Apple(Clone)")
                {
                    score += 10;
                    scoreText.text = "Score: " + score;
                }

                if(currentObject.name == "Gold Bar(Clone)")
                {
                    score += 100;

                    scoreText.text = "Score: " + score;
                }

                if(currentObject.name == "Fly(Clone)")
                {
                    score += 1;

                    scoreText.text = "Score: " + score;
                }

                Destroy(currentObject);

            }


            if(objectsBody.position.y < 0.2f)
            {
                Destroy(currentObject);
            }
            
            if(catchingTimer > 30f)
            {
                catchingStarted = false;
                characterMovementScript.enabled = true;
                timerText.text = "Done!";
            }


            

        }






        
    }

    private void OnCollisionEnter(Collision playerCollider)
    {
        //Set position to area where gun duel happens
        playerBody.linearVelocity = Vector3.zero;
        //playerBody.angularVelocity = Vector3.zero;

        playerBody.rotation = Quaternion.identity;

        playerObj.transform.localPosition = new Vector3(clappingPosition.transform.position.x, clappingPosition.transform.position.y, clappingPosition.transform.position.z - 1);
        Camera.main.transform.localRotation = Quaternion.identity;
        characterMovementScript.enabled = false;
        //Time draw is random between 5-15 seconds

        timerText.enabled = true;
        scoreText.enabled = true;

        scoreText.text = "Score: ";

        listPosition = 0;

        score = 0;

        catchingStarted = true;
        catchingTimer = 0f;
    }
}
