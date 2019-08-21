using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incomingCircle : MonoBehaviour
{
    bubble[] myBubbles;
    [SerializeField] float speed = 1.1f;

    bool isRotating = false;
    const float ROTATION_SPEED = 1f;
    [SerializeField] float targetAngle = 0;
    float counter = 0;
    float prevAngle = 0;

    // Start is called before the first frame update
    void Start()
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

    void RotateLeft()
    {
        counter = 0;
        prevAngle = this.transform.eulerAngles.z;
        targetAngle += 45;
        FixTargetAngle();
        isRotating = true;
    }

    void RotateRight()
    {
        counter = 0;
        prevAngle = this.transform.eulerAngles.z;
        targetAngle += 45;
        FixTargetAngle();
        isRotating = true;
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

    public void RotateArrayLeft()
    {
        bubble temp = myBubbles[0];
        for (int i = 0; i < myBubbles.Length - 1; i++)
        {
            myBubbles[i] = myBubbles[i+1];
        }
        myBubbles[myBubbles.Length - 1] = temp;
    }

    public void RotateArrayRight()
    {
        bubble temp = myBubbles[myBubbles.Length - 1];
        for (int i = myBubbles.Length - 1; i > 0; i--)
        {
            myBubbles[i] = myBubbles[i - 1];
        }
        myBubbles[0] = temp;
    }
}
