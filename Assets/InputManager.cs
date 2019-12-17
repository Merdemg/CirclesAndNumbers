using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject pivotObj;
    float rotationSpeed = .35f;
    Quaternion defaultRot;
    float lastLength = 0;
    Vector2 centre;
    ESwipeDirection lastSwipeDir;


    // Start is called before the first frame update
    void Start()
    {
        SwipeController.OnSwipe += GetSwipe;
        defaultRot = pivotObj.transform.rotation;
        centre = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetSwipe(SwipeInfo info)
    {
        pivotObj.transform.rotation = defaultRot;
        float length = Vector2.Distance(info.SwipeStartPos, info.SwipeEndPos);

        if (info.isEnd || length < lastLength || (lastLength != 0 && lastSwipeDir != info.SwipeDirection))
        {
            defaultRot = pivotObj.transform.rotation;
            lastLength = 0;
        }
        else
        {
            lastLength = length;
        }

        lastSwipeDir = info.SwipeDirection;

        float multiplier = 1f;

        if(Vector2.SignedAngle(info.SwipeStartPos - centre, info.SwipeEndPos - centre) < 0)
        {
            multiplier *= -1;
        }

        pivotObj.transform.Rotate(Vector3.forward, length * rotationSpeed * multiplier);
    }
}
