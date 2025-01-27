using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleManager : MonoBehaviour
{
    public static bubbleManager instance { get; private set; }

    [SerializeField] int maxBubbles;

    [SerializeField] GameObject bubblePrefab;

    public List<bubbleBehavior> p1Bubbles = new List<bubbleBehavior>();
    public List<bubbleBehavior> p2Bubbles = new List<bubbleBehavior>();

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

        foreach (bubbleBehavior bubble in FindObjectsOfType<bubbleBehavior>())
        {
            if (bubble.playerTeam == 0)
            {
                bubble.GetComponent<SpriteRenderer>().color = gameManager.instance.p1Color;
                p1Bubbles.Add(bubble);
            }
            else if (bubble.playerTeam == 1)
            {
                bubble.GetComponent<SpriteRenderer>().color = gameManager.instance.p2Color;
                p2Bubbles.Add(bubble);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBubble(int playerNum, Vector3 pos)
    {

        if(playerNum == 0 && p1Bubbles.Count < maxBubbles && gameManager.instance.gameActive)
        {
            GameObject newBubble = Instantiate(bubblePrefab, pos, Quaternion.identity);
            bubbleBehavior bubbleRef = newBubble.GetComponent<bubbleBehavior>();

            bubbleRef.initializeBubble(playerNum, gameManager.instance.p1Color);
            p1Bubbles.Add(bubbleRef);
        }
        else if(playerNum == 1 && p2Bubbles.Count < maxBubbles && gameManager.instance.gameActive)
        {
            GameObject newBubble = Instantiate(bubblePrefab, pos, Quaternion.identity);
            bubbleBehavior bubbleRef = newBubble.GetComponent<bubbleBehavior>();

            bubbleRef.initializeBubble(playerNum, gameManager.instance.p2Color);
            p2Bubbles.Add(bubbleRef);
        }
    }

    public GameObject pickNewBubble(int playerNum, bubbleBehavior currentBubble)
    {
        if(playerNum == 0 && p1Bubbles.Count > 1)
        {
            List<bubbleBehavior> availableBubbles = new List<bubbleBehavior>();
            foreach (bubbleBehavior b in p1Bubbles)
            {
                if(b != currentBubble && !b.inOppEndzone)
                {
                    availableBubbles.Add(b);
                }
            }

            if(availableBubbles.Count >= 1)
            {
                //List<bubbleBehavior> availableBubbles = p1Bubbles;
                //availableBubbles.Remove(currentBubble);
                int bubbleIndex = Random.Range(0, availableBubbles.Count);
                return availableBubbles[bubbleIndex].gameObject;
            }
            else
            {
                return null;
            }
        }
        else if(playerNum == 1 && p2Bubbles.Count > 1)
        {
            List<bubbleBehavior> availableBubbles = new List<bubbleBehavior>();
            foreach (bubbleBehavior b in p2Bubbles)
            {
                if (b != currentBubble && !b.inOppEndzone)
                {
                    availableBubbles.Add(b);
                }
            }

            if (availableBubbles.Count >= 1)
            {
                //List<bubbleBehavior> availableBubbles = p2Bubbles;
                //availableBubbles.Remove(currentBubble);
                int bubbleIndex = Random.Range(0, availableBubbles.Count);
                return availableBubbles[bubbleIndex].gameObject;
            }
            else
            {
                return null;
            }

        }
        else
        {
            return null;
        }
    }

    public void destroyBubble(GameObject bubble, int playerNum)
    {
        bubbleBehavior bubbleRef = bubble.GetComponent<bubbleBehavior>();

        if(playerNum == 0)
        {
            p1Bubbles.Remove(bubbleRef);
        }
        else if(playerNum == 1)
        {
            p2Bubbles.Remove(bubbleRef);
        }

        Destroy(bubble);
    }

    public void resetBubbles()
    {
        p1Bubbles.Clear();
        p2Bubbles.Clear();

        bubbleBehavior[] activeBubbles = FindObjectsOfType<bubbleBehavior>();
        for (int i = 0; i < activeBubbles.Length; i++)
        {
            if (activeBubbles[i].GetComponentInChildren<player>() != null)
            {
                if(activeBubbles[i].GetComponentInChildren<player>().getPlayerIndex() == 0)
                {
                    p1Bubbles.Add(activeBubbles[i]);
                    activeBubbles[i].gameObject.transform.position = gameManager.instance.p1Start.position;
                }
                else
                {
                    p2Bubbles.Add(activeBubbles[i]);
                    activeBubbles[i].gameObject.transform.position = gameManager.instance.p2Start.position;
                }
            }
            else
            {
                Destroy(activeBubbles[i].gameObject);
            }
        }

        //for (int i = 0; i < p1Bubbles.Count; i++)
        //{
        //    if (p1Bubbles[i].GetComponentInChildren<player>() != null)
        //    {
        //        //reset pos
        //        p1Bubbles[i].gameObject.transform.position = gameManager.instance.p1Start.position;
        //    }
        //    else
        //    {
        //        destroyBubble(p1Bubbles[i].gameObject, 0);
        //    }
        //}

        //for (int i = 0; i < p2Bubbles.Count; i++)
        //{
        //    if (p2Bubbles[i].GetComponentInChildren<player>() != null)
        //    {
        //        //reset pos
        //        p2Bubbles[i].gameObject.transform.position = gameManager.instance.p2Start.position;
        //    }
        //    else
        //    {
        //        destroyBubble(p2Bubbles[i].gameObject, 1);
        //    }
        //}
    }
}
