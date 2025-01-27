using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance { get; private set; }

    public bool controllerGameMain = false;
    public bool keyboardGameMain = false;

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

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void startControllerGame()
    {
        controllerGameMain = true;
        keyboardGameMain = false;
        changeScene("Game Test");
    }

    public void startKeyboardGame()
    {
        controllerGameMain = false;
        keyboardGameMain = true;
        changeScene("Game Test");
    }
}
