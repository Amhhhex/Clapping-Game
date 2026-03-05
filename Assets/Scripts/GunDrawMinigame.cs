using UnityEngine;

public class GunDrawMinigame : MonoBehaviour
{
    public HandCollision leftHandCollidedScr;
    public HandCollision rightHandCollidedScr;
    public GameObject playerObj;
    public GameObject gunObj;
    ArmMovement armMovementScr;
    CharacterMovement characterMovementScr;
    float currentTimeInDraw;
    float currentTimeInShowdown;
    float totalDrawTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armMovementScr = playerObj.GetComponentInChildren<ArmMovement>();
        characterMovementScr = playerObj.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the arm leaves the gun, the collision is reset
        if (armMovementScr.returnLeftHand) { leftHandCollidedScr.handCollidedObj = null; }
        if (armMovementScr.returnRightHand) { rightHandCollidedScr.handCollidedObj = null; }
    }

    private void OnCollisionEnter(Collision playerCollider)
    {
        //Set position to area where gun duel happens
        playerObj.transform.localPosition = new Vector3(gunObj.transform.position.x, playerObj.transform.position.y, gunObj.transform.position.z - 1);
        Camera.main.transform.localRotation = Quaternion.identity;
        characterMovementScr.enabled = false;

        //Time draw is random between 5-15 seconds
        totalDrawTime = Random.Range(5, 15);
        currentTimeInDraw += Time.deltaTime;

        if (currentTimeInDraw >= totalDrawTime) 
        {
            currentTimeInShowdown += Time.deltaTime;
            if ( currentTimeInShowdown >= totalDrawTime)
            {
                //Play gunshot
                Debug.Log("You lose");
            }
        }

        if (leftHandCollidedScr.handCollidedObj == gunObj && leftHandCollidedScr.handCollidedObj == gunObj)
        {
            Debug.Log("You win");
            //Play gunshot
            //Spin Gunslinger
            //Assume new position
            characterMovementScr.enabled = true;
        }
    }
}
