using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incomingCircle : MonoBehaviour
{
    bubble[] myBubbles;
    [SerializeField] float speed = 1.1f;

    bool isRotating = false;
    const float ROTATION_SPEED = 2f;
    [SerializeField] float targetAngle = 0;
    float counter = 0;
    float prevAngle = 0;

    // Start is called before the first frame update
    void Awake()
    {
        myBubbles = GetComponentsInChildren<bubble>();
    }

    // Update is called once per frame
    void Update()
    {
        fakeMovement();

        if (isRotating)
        {
            counter += Time.deltaTime * ROTATION_SPEED;
            if (counter >= 1)
            {
                counter = 1;
                isRotating = false;
            }

            float angle = Mathf.LerpAngle(prevAngle, targetAngle, counter);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }


        //debug
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateRight();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateLeft();
        }
    }

    void fakeMovement()
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
        counter = 0;
        prevAngle = this.transform.eulerAngles.z;
        targetAngle += 45;
        FixTargetAngle();
        isRotating = true;

        RotateArrayLeft();
    }

    public void RotateRight()
    {
        counter = 0;
        prevAngle = this.transform.eulerAngles.z;
        targetAngle -= 45;
        FixTargetAngle();
        isRotating = true;

        RotateArrayRight();
    }

    void FixTargetAngle()
    {
        while (targetAngle >= 360)
        {
            targetAngle -= 360;
        }

        while (targetAngle < 0)
        {
            targetAngle += 360;
        }
    }

    void RotateArrayLeft()
    {
        //bubble temp = myBubbles[0];
        //for (int i = 0; i < myBubbles.Length - 1; i++)
        //{
        //    myBubbles[i] = myBubbles[i+1];
        //}
        //myBubbles[myBubbles.Length - 1] = temp;

        foreach (bubble b in myBubbles)
        {
            b.rotateLeft();
        }
    }

    void RotateArrayRight()
    {
        //bubble temp = myBubbles[myBubbles.Length - 1];
        //for (int i = myBubbles.Length - 1; i > 0; i--)
        //{
        //    myBubbles[i] = myBubbles[i - 1];
        //}
        //myBubbles[0] = temp;
        foreach (bubble b in myBubbles)
        {
            b.rotateRight();
        }
    }
}
