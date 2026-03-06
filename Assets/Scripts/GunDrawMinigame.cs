using UnityEngine;
using TMPro;

public class GunDrawMinigame : MonoBehaviour
{
    public HandCollision leftHandCollidedScr;
    public HandCollision rightHandCollidedScr;
    public GameObject playerObj;
    public GameObject bananaObj;
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
    float currentResultsTime;
    float totalDrawTime;
    float totalShowdownTime = 2f;
    bool showdownStarted;
    bool showdownWon;

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

                if (leftHandCollidedScr.handCollidedObj == bananaObj && rightHandCollidedScr.handCollidedObj == bananaObj || showdownWon)
                {
                    drawAnnounceText.text = "You win!";
                    showdownWon = true;
                    currentTimeInShowdown = 0;
                    currentResultsTime += Time.deltaTime;
                    bananaSprRndr.sprite = bananaShotSpr;
                    gunmanSprRndr.sprite = gunmanDownSpr;
                    if (currentResultsTime > 3)
                    {
                        currentTimeInDraw = 0;
                        currentResultsTime = 0;
                        showdownStarted = false;
                        showdownWon = false;
                        gunmanSprRndr.sprite = gunmanNeutralSpr;
                        billboardScr.enabled = true;
                        characterMovementScr.enabled = true;
                        bananaObj.gameObject.SetActive(false);
                        drawAnnounceText.gameObject.SetActive(false);
                    }
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
                        gunmanSprRndr.sprite = gunmanNeutralSpr;
                        billboardScr.enabled = false;
                        characterMovementScr.enabled = true;
                        bananaObj.gameObject.SetActive(false);
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
        playerObj.transform.position = duelPlayerSpawn.transform.position;
        Camera.main.transform.eulerAngles = new Vector3(0, 180, 0);
        characterMovementScr.enabled = false;
        bananaObj.gameObject.SetActive(true);
        drawAnnounceText.enabled = true;
        //Time draw is random between 5-15 seconds
        totalDrawTime = Random.Range(3, 10);
        showdownStarted = true;
    }
}
