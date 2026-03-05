using UnityEngine;
using TMPro;

public class GunDrawMinigame : MonoBehaviour
{
    public HandCollision leftHandCollidedScr;
    public HandCollision rightHandCollidedScr;
    public GameObject playerObj;
    public GameObject gunObj;
    public GameObject duelPlayerSpawn;
    public TextMeshProUGUI drawAnnounceText;
    public Sprite bananaShotSpr;
    public Sprite bananaLoadedSpr;
    public Sprite gunmanNeutralSpr;
    public Sprite gunmanDrawSpr;
    public Sprite gunmanDownSpr;
    SpriteRenderer bananaSprRndr;
    SpriteRenderer gunmanSprRndr;
    BillboardSprite billboardScr;
    Rigidbody playerBody;
    ArmMovement armMovementScr;
    CharacterMovement characterMovementScr;
    float currentTimeInDraw;
    float currentTimeInShowdown;
    float totalDrawTime;
    float totalShowdownTime = 2f;
    bool showdownStarted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armMovementScr = playerObj.GetComponentInChildren<ArmMovement>();
        characterMovementScr = playerObj.GetComponent<CharacterMovement>();
        playerBody = playerObj.GetComponent<Rigidbody>();
        bananaSprRndr = GetComponentInChildren<SpriteRenderer>();
        gunmanSprRndr = GetComponent<SpriteRenderer>();
        billboardScr = GetComponent<BillboardSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the arm leaves the gun, the collision is reset
        if (armMovementScr.returnLeftHand) { leftHandCollidedScr.handCollidedObj = null; }
        if (armMovementScr.returnRightHand) { rightHandCollidedScr.handCollidedObj = null; }

        if (showdownStarted)
        {
            currentTimeInDraw += Time.deltaTime;
            drawAnnounceText.text = "Ready your arms..!";
            if (currentTimeInDraw >= totalDrawTime)
            {
                drawAnnounceText.text = "DRAW!";
                drawAnnounceText.gameObject.SetActive(true);
                gunmanSprRndr.sprite = gunmanDrawSpr;
                currentTimeInShowdown += Time.deltaTime;

                if (leftHandCollidedScr.handCollidedObj == gunObj && rightHandCollidedScr.handCollidedObj == gunObj)
                {
                    drawAnnounceText.text = "You win!";
                    //Play gunshot
                    //Spin Gunslinger
                    //Assume new position
                    bananaSprRndr.sprite = bananaShotSpr;
                    gunmanSprRndr.sprite = gunmanDownSpr;
                    currentTimeInDraw = 0;
                    currentTimeInShowdown = 0;
                    showdownStarted = false;
                    billboardScr.enabled = false;
                    characterMovementScr.enabled = true;
                }

                if (currentTimeInShowdown >= totalShowdownTime)
                {
                    //Play gunshot
                    drawAnnounceText.text = "Death to you...";
                    if (currentTimeInShowdown >= totalShowdownTime + 2)
                    {
                        currentTimeInDraw = 0;
                        currentTimeInShowdown = 0;
                        showdownStarted = false;
                        billboardScr.enabled = false;
                        characterMovementScr.enabled = true;
                        drawAnnounceText.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision playerCollider)
    {
        //Set position to area where gun duel happens
        billboardScr.enabled = false;
        transform.eulerAngles = Vector3.zero;
        bananaSprRndr.sprite = bananaLoadedSpr;
        gunmanSprRndr.sprite = gunmanNeutralSpr;
        playerObj.transform.position = duelPlayerSpawn.transform.localPosition;
        Camera.main.transform.localRotation = Quaternion.identity;
        characterMovementScr.enabled = false;
        //Time draw is random between 5-15 seconds
        totalDrawTime = Random.Range(5, 15);
        showdownStarted = true;
    }
}
