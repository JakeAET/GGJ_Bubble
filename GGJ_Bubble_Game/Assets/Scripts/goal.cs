using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    [SerializeField] int playerNum = 0;
    [SerializeField] SpriteRenderer endZone;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(playerNum == 0)
        {
            Color goalColor = gameManager.instance.p1Color;
            goalColor.a = 0.3f;
            //sprite.color = goalColor;
            goalColor.a = 0.1f;
            endZone.color = goalColor;
        }
        else if(playerNum == 1)
        {
            Color goalColor = gameManager.instance.p2Color;
            goalColor.a = 0.3f;
            //sprite.color = goalColor;
            goalColor.a = 0.1f;
            endZone.color = goalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
