using System;
using UnityEngine;

/*
*  SwipeController.cs - Main script for detecting user swipes on screen.
*  Once a swipe is detected, we check to see whether it's a horizontal or vertical swipe.
*  After that, we check the direction in which the user is swiping.
*  This depends on the start, and end positions of the touch.
*/

// Struct holding useful data for a user's swipe.
// We have the starting and ending positions of a swipe, and the direction.
public struct SwipeInfo
{
    public Vector2 SwipeStartPos;
    public Vector2 SwipeEndPos;
    public ESwipeDirection SwipeDirection;
}

// Enum holding the directions in which a user can swipe.
public enum ESwipeDirection
{
    Swipe_UP,
    Swipe_Down,
    Swipe_Left,
    Swipe_Right
}

public class SwipeController : MonoBehaviour
{
    // Distance of the trail renderer from the camera, in world space.
    // Should be kept between 0.5f, and 1f.
    public float distanceFromCamera = 0.5f;

    // GameObject holding the trail renderer, that visually represents the swipe.
    [SerializeField] private GameObject m_tracerObj = null;

    private TrailRenderer m_tracerRenderer = null;

    private Vector2 m_touchDownPos = Vector2.zero;
    private Vector2 m_touchUpPos = Vector2.zero;

    // Set to false if you don't want to have visual feedback for swipes.
    [SerializeField] private bool m_bIsTracerActive = true;

    [SerializeField] private float m_minSwipeThreshold = 0.0f;

    //  Static event for when a user swipes on screen.
    //
    //  [Example usage on any script]
    //
    //  void Awake()
    //  {
    //      SwipeController.OnSwipe += MethodName_OnSwipe;
    //  }
    //
    //  void MethodName_OnSwipe(SwipeInfo info){}
    //
    //  NOTE: _OnSwipe is just to emphasize on the fact that it's a delegate to the OnSwipe event.
    //
    public static event Action<SwipeInfo> OnSwipe = delegate { };

    private void Start()
    {
        m_tracerRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        /*
         *  Check touch phases and detect user's swipe.
         */
        foreach (Touch touch in Input.touches)
        {
            // If the trail renderer is active, update the position.
            // Once updated, project in world space.
            if (m_bIsTracerActive)
            {
                Vector3 tracerPosition = touch.position;
                tracerPosition.z = distanceFromCamera;
                Vector3 vec3 = Camera.main.ScreenToWorldPoint(tracerPosition);

                if (m_tracerObj)
                {
                    m_tracerObj.transform.position = vec3;
                }
            }

            if (touch.phase == TouchPhase.Began)
            {
                m_touchUpPos = touch.position;
                m_touchDownPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                m_touchUpPos = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                m_touchUpPos = touch.position;
                DetectSwipe();
            }
        }
    }

    /*
    * Set swipe direction based on y and x values of end - start.
    */
    private void DetectSwipe()
    {
        if (VertDist() > m_minSwipeThreshold || HorDist() > m_minSwipeThreshold)
        {
            if (IsVertSwipe())
            {
                ESwipeDirection dir = m_touchDownPos.y - m_touchUpPos.y > 0 ? ESwipeDirection.Swipe_Down : ESwipeDirection.Swipe_UP;
                SendSwipeData(dir);
            }
            else
            {
                ESwipeDirection dir = m_touchDownPos.x - m_touchUpPos.x > 0 ? ESwipeDirection.Swipe_Left : ESwipeDirection.Swipe_Right;
                SendSwipeData(dir);
            }

            m_touchUpPos = m_touchDownPos;
        }
    }

    // Once the swipe has been detected and it's ready, send the data.
    // The data sent is retrieved by the registered delegates.
    private void SendSwipeData(ESwipeDirection dir)
    {
        SwipeInfo swipeInfo = new SwipeInfo()
        {
            SwipeDirection = dir,
            SwipeStartPos = m_touchDownPos,
            SwipeEndPos = m_touchUpPos
        };

        OnSwipe(swipeInfo);
    }

    // Check if the swipe is vertical.
    public bool IsVertSwipe()
    {
        return VertDist() > HorDist();
    }

    // Returns the vertical distance of the swipe.
    private float VertDist()
    {
        return Mathf.Abs(m_touchDownPos.y - m_touchUpPos.y);
    }

    // Returns the horizontal distance of the swipe.
    private float HorDist()
    {
        return Mathf.Abs(m_touchDownPos.x - m_touchUpPos.x);
    }

    // Sets the event to null, if needed.
    public void ClearOnSwipe()
    {
        OnSwipe = null;
    }
}
