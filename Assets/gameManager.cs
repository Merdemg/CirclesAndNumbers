using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject incCirclePrefab;
    incomingCircle currentIncoming;

    // Start is called before the first frame update
    void Start()
    {
        SwipeController.OnSwipe += GetSwipe;
        currentIncoming = FindObjectOfType<incomingCircle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetSwipe(SwipeInfo info)
    {
        if (info.SwipeDirection == ESwipeDirection.Swipe_Left)
        {
            currentIncoming.RotateLeft();
        }else if (info.SwipeDirection == ESwipeDirection.Swipe_Right)
        {
            currentIncoming.RotateRight();
        }
    }

    public void SpawnIncCircle()
    {
        Instantiate(incCirclePrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("spawnd");
    }
}
