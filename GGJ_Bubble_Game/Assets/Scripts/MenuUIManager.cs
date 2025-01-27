using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject controlInfoPanel;
    [SerializeField] GameObject controlSelectPanel;

    [SerializeField] GameObject defaultBG1;
    [SerializeField] GameObject defaultBG2;

    [SerializeField] GameObject boysKissing;

    // Start is called before the first frame update
    void Start()
    {
        float randChance = Random.Range(0, 6);
        if(randChance == 5)
        {
            boysKissing.SetActive(true);
            defaultBG1.SetActive(false);
            defaultBG2.SetActive(false);
        }
        else
        {
            boysKissing.SetActive(false);
            defaultBG1.SetActive(true);
            defaultBG2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton()
    {
        controlSelectPanel.SetActive(true);
    }

    public void exitButton()
    {
        Application.Quit();
    }

    public void controlsButton()
    {
        controlInfoPanel.SetActive(true);
    }

    public void keyboardGameButton()
    {
        SceneController.instance.startKeyboardGame();
    }

    public void controllerGameButton()
    {
        SceneController.instance.startControllerGame();
    }

    //public void controllerInfoButton()
    //{
    //    controllerInfoImage.SetActive(true);
    //    keyboardInfoImage.SetActive(false);

    //}
}
