using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] TMP_Text p1ScoreText;
    [SerializeField] TMP_Text p2ScoreText;

    [SerializeField] GameObject winScreen;

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

        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        p1ScoreText.color = gameManager.instance.p1Color;
        p2ScoreText.color = gameManager.instance.p2Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateP1Score(int score)
    {
        p1ScoreText.text = "" + score;
    }

    public void updateP2Score(int score)
    {
        p2ScoreText.text = "" + score;

    }

    public void restartGame()
    {
        gameManager.instance.gameStart();
    }

    public void exitGame()
    {
        SceneController.instance.changeScene("Main Menu");
    }
}
