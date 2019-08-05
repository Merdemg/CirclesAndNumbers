using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SwipeController.OnSwipe += GetSwipe;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetSwipe(SwipeInfo info)
    {
        if (info.SwipeDirection == ESwipeDirection.Swipe_Left)
        {

        }
    }
}
