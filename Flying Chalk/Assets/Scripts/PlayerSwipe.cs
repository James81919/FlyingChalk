using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    private void Awake()
    {
        SwipeDetector.OnSwipe += RedirectChalk;
    }

    private void RedirectChalk(SwipeData data)
    {
        Vector2 swipeDirection;
        swipeDirection = (data.EndPosition - data.StartPosition).normalized;
        Debug.Log("Swipe Direction = " + swipeDirection);
    }
}
