using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maincircle : MonoBehaviour
{
    bubble[] myBubbles;
    gameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        myBubbles = GetComponentsInChildren<bubble>();
        manager = FindObjectOfType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {   // DOESNT WORK!!!
        Debug.Log("bla");

        if (collision.gameObject.GetComponent<incomingCircle>())
        {
            foreach (bubble b in myBubbles)
            {
                if (b.getIsActive())
                {
                    foreach (bubble colliderB in collision.gameObject.GetComponent<incomingCircle>().getBubbles())
                    {
                        if (b.getPosition() == colliderB.getPosition())
                        {
                            b.collide(colliderB.getValue());
                        }
                    }
                }
            }

            Destroy(collision.gameObject);
            manager.SpawnIncCircle();
        }
    }


    public void collide(incomingCircle other)
    {
        foreach (bubble b in myBubbles)
        {
            if (b.getIsActive())
            {
                foreach (bubble colliderB in other.getBubbles())
                {
                    if (b.getPosition() == colliderB.getPosition())
                    {
                        b.collide(colliderB.getValue());
                    }
                }
            }
        }

        Destroy(other.gameObject);
        manager.SpawnIncCircle();
    }
}
