using System;
using UnityEngine;

// Stores all the data of the swipe
public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

// The directions the swipe could be
public enum SwipeDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class SwipeDetector : MonoBehaviour
{
    // The position where the drag begins
    private Vector2 fingerDownPosition;
    // The position where the drag ends/currently is
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    // The minimum swipe distance to detect swipe
    [SerializeField]
    private float minDistanceForSwipe = 20f;

    // The swipe delegate
    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update()
    {
#if UNITY_EDITOR
        // On drag begin with mouse
        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPosition = Input.mousePosition;
            fingerDownPosition = Input.mousePosition;
        }

        // While dragging with mouse
        if (!detectSwipeOnlyAfterRelease && Input.GetMouseButton(0))
        {
            fingerDownPosition = Input.mousePosition;
            DetectSwipe();
        }

        // On drag end with mouse
        if (Input.GetMouseButtonUp(0))
        {
            fingerDownPosition = Input.mousePosition;
            DetectSwipe();
        }
#else
        foreach (Touch touch in Input.touches)
        {
            // On drag begin
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }
            
            // While dragging
            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            // On drag end
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
#endif
    }

    // Called to detect if there was a swipe
    private void DetectSwipe()
    {
        // If swipe distance is long enough
        if (SwipeDistanceCheckMet())
        {
            // If is a vertical swipe
            if (IsVerticalSwipe())
            {
                // Set swipe direction to up or down
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.UP : SwipeDirection.DOWN;
                SendSwipe(direction);
            }
            else // If is a horizontal swipe
            {
                // Set swipe direction to left or right
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }


    // Checks if the swipe direction is vertical or horizontal
    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    // Checks to see if the swipe distance is long enough to be counted as a swipe
    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    // Gets the vertical swipe distance
    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    // Gets the horizontal swipe distance
    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    // Updates the swipe data and calls the onSwipe delegate
    private void SendSwipe(SwipeDirection direction)
    {
        // Setting all swipe data
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        // Calling swipe delegate
        OnSwipe(swipeData);
    }
}
