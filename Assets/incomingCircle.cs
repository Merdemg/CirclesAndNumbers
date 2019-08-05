using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incomingCircle : MonoBehaviour
{
    bubble[] myBubbles;
    [SerializeField] float speed = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        myBubbles = GetComponentsInChildren<bubble>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale + (transform.localScale * Time.deltaTime * speed);

        if (transform.localScale.x >= 1.05f)
        {
            FindObjectOfType<maincircle>().collide(this);
        }
    }

    public bubble[] getBubbles()
    {
        return myBubbles;
    }

    public void RotateLeft()
    {
        bubble temp = myBubbles[0];
        for (int i = 0; i < myBubbles.Length - 1; i++)
        {
            myBubbles[i] = myBubbles[i+1];
        }
        myBubbles[myBubbles.Length - 1] = temp;
    }

    public void RotateRight()
    {
        bubble temp = myBubbles[myBubbles.Length - 1];
        for (int i = myBubbles.Length - 1; i > 0; i--)
        {
            myBubbles[i] = myBubbles[i - 1];
        }
        myBubbles[0] = temp;
    }
}
