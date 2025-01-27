using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance { get; private set; }

    [SerializeField] int pointGoal;
    public bool controllerGame;
    public bool keyboardGame;

    [SerializeField] GameObject p1KeyboardPlayer;
    [SerializeField] GameObject p2KeyboardPlayer;

    [SerializeField] GameObject controllerInputManager;
    [SerializeField] GameObject keyboardInputManager;

    public Color p1Color;
    public Color p2Color;

    public int p1Points;
    public int p2Points;

    [SerializeField] player player1Ref;
    [SerializeField] player player2Ref;

    public Transform p1Start;
    public Transform p2Start;

    public bool gameActive = false;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject pinkWinScreen;
    [SerializeField] GameObject greenWinScreen;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceStart;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        keyboardGame = SceneController.instance.keyboardGameMain;
        controllerGame = SceneController.instance.controllerGameMain;

        if (keyboardGame)
        {
            keyboardInputManager.SetActive(true);
            p1KeyboardPlayer.SetActive(true);
            p2KeyboardPlayer.SetActive(true);
        }
        else if (controllerGame)
        {
            controllerInputManager.SetActive(true);
        }


        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        initializeGame();
        //if (keyboardGame)
        //{
        //    p1KeyboardPlayer.transform.SetParent(GameObject.FindGameObjectWithTag("p1StartBubble").transform);
        //    p2KeyboardPlayer.transform.SetParent(GameObject.FindGameObjectWithTag("p2StartBubble").transform);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (gameActive)
        //    {
        //        gameEnd();
        //    }
        //    else
        //    {
        //        gameStart();
        //    }
        //}
    }

    public void goalScored(int playerNum)
    {
        if (gameActive)
        {
            audioSource.Play();

            if (playerNum == 0)
            {
                p1Points++;
                UIManager.instance.updateP1Score(p1Points);
                //Debug.Log("Player one scored! P1: " + p1Points);
                //pinkWinScreen.SetActive(true);
            }
            if (playerNum == 1)
            {
                p2Points++;
                UIManager.instance.updateP2Score(p2Points);
                //Debug.Log("Player two scored! P2: " + p2Points);
                //greenWinScreen.SetActive(true);
            }

            if (p1Points >= pointGoal)
            {
                pinkWinScreen.SetActive(true);
                gameEnd();
            }

            if (p2Points >= pointGoal)
            {
                greenWinScreen.SetActive(true);
                gameEnd();
            }
        }
    }

    public void gameEnd()
    {
        gameActive = false;
        Debug.Log("Game End");

        winScreen.SetActive(true);

        if (keyboardGame)
        {
            keyboardInputManager.SetActive(false);
        }
        else if (controllerGame)
        {
            controllerInputManager.SetActive(false);
        }

        // reset bubbles
        bubbleManager.instance.resetBubbles();

        // disable movement
        player1Ref.disableMovement();
        player2Ref.disableMovement();
    }

    public void gameStart()
    {
        gameActive = true;
        Debug.Log("Game Start");

        audioSourceStart.Play();

        if (keyboardGame)
        {
            keyboardInputManager.SetActive(true);
        }
        else if (controllerGame)
        {
            controllerInputManager.SetActive(true);
        }

        p1Points = 0;
        p2Points = 0;
        UIManager.instance.updateP1Score(0);
        UIManager.instance.updateP2Score(0);

        winScreen.SetActive(false);
        pinkWinScreen.SetActive(false);
        greenWinScreen.SetActive(false);


        // start sequence and countdown
        // enable movement
        player1Ref.enableMovement();
        player2Ref.enableMovement();
    }

    private void initializeGame()
    {
        player1Ref.disableMovement();
        player2Ref.disableMovement();
    }
}
