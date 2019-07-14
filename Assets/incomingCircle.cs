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
}
