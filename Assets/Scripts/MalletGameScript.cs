using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class MalletGameScript : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject malletPosition;

    public Camera PlayerCamera;

    Rigidbody playerBody;

    public float highestPosition;
    public float currentPosition;


    ArmMovement armMovementScr;
    CharacterMovement characterMovementScript;

    bool malletStarted;

    public float malletTimer;


    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highestPositionText;


    public InputActionReference CameraP1Input;
    public InputActionReference CameraP2Input;
    public InputActionReference ClappingP1Input;
    public InputActionReference ClappingP2Input;


    int rotationSpeed = 30;
    Vector2 CameraDirection;


    int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        armMovementScr = playerObj.GetComponentInChildren<ArmMovement>();
        characterMovementScript = playerObj.GetComponent<CharacterMovement>();

        playerBody = playerObj.GetComponent<Rigidbody>();

        

    }

    // Update is called once per frame
    void Update()
    {

        if(malletStarted)
        {
            malletTimer -= Time.deltaTime;

            

            timerText.text = "Time Left: " + ((int)malletTimer).ToString();

            CameraMovement();

            PlayerMovement();

            currentPosition = playerBody.transform.position.y;


            if(currentPosition > highestPosition)
            {

                highestPosition = currentPosition;

                highestPositionText.text = "Your highest position: " + highestPosition;

            }


            if(malletTimer < 0f)
            {


                playerObj.transform.position = malletPosition.transform.position;


                playerBody.linearDamping = 2f;

                playerBody.constraints = RigidbodyConstraints.FreezePositionY;
                playerBody.constraints = RigidbodyConstraints.FreezeRotation;
                characterMovementScript.enabled = true;

                malletStarted = false;
                


            }


        }

        
        
    }

    void PlayerMovement()
    {
        if (armMovementScr.clapCheck == true && armMovementScr.clapScore > 50f)
        {
            playerBody.AddForce(PlayerCamera.transform.up * armMovementScr.clapScore, ForceMode.Impulse);
        }
    }

    void CameraMovement()
    {
        CameraDirection = new Vector2(-CameraP1Input.action.ReadValue<Vector2>().y + -CameraP2Input.action.ReadValue<Vector2>().y, CameraP1Input.action.ReadValue<Vector2>().x + CameraP2Input.action.ReadValue<Vector2>().x);
        PlayerCamera.transform.eulerAngles = PlayerCamera.transform.eulerAngles + (Vector3)CameraDirection * rotationSpeed * Time.deltaTime;
    }



    private void OnCollisionEnter(Collision playerCollider)
    {
        //Set position to area where gun duel happens
        playerBody.linearVelocity = Vector3.zero;
        //playerBody.angularVelocity = Vector3.zero;

        playerBody.rotation = Quaternion.identity;

        playerBody.linearDamping = 6f;

        playerObj.transform.localPosition = new Vector3(malletPosition.transform.position.x, malletPosition.transform.position.y, malletPosition.transform.position.z);
        Camera.main.transform.localRotation = Quaternion.identity;
        characterMovementScript.enabled = false;
        //Time draw is random between 5-15 seconds

        timerText.enabled = true;
        highestPositionText.enabled = true;

        highestPositionText.text = "Your highest position: ";


        playerBody.constraints = RigidbodyConstraints.None;

        score = 0;

        highestPosition = 0;

        malletStarted = true;
        malletTimer = 30f;
    }
}
