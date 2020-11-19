using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    // The selected chalk in range
    private GameObject currentChalk;

    public GameObject[] students;
    public List<Vector2> studentDirections;

    private void Awake()
    {
        // Set the onSwipe delegate
        SwipeDetector.OnSwipe += RedirectChalk;

        // Updates all the student directions
        UpdateAllStudentDirections();
    }

    // Called to set the current chalk
    public void SetCurrentChalk(GameObject chalk)
    {
        currentChalk = chalk;
    }

    // Called to update all student direction vectors (player to student)
    private void UpdateAllStudentDirections()
    {
        // Resets all direction vectors
        studentDirections = new List<Vector2>();

        // Loops through all students
        for (int i = 0; i < students.Length; i++)
        {
            // Adds vector from player to student (screen space)
            studentDirections.Add((Camera.main.WorldToScreenPoint(students[i].transform.position) - Camera.main.WorldToScreenPoint(transform.position)).normalized);
        }
    }

    // Checks to see which student direction vector is closest to the swipe direction vector
    private int GetClosestStudentDirectionIndex(Vector2 swipeDirection)
    {
        // The index of the currently closest student
        int index = 0;
        // The closest current distance between vectors
        float lowestValue = Vector2.Distance(swipeDirection, studentDirections[0]);

        // Loops through all vectors
        for (int i = 1; i < studentDirections.Count; i++)
        {
            // If student direction is the same as the swipe direction...
            if (studentDirections[i] == swipeDirection)
            {
                // Set this student as closest student and finish check
                index = i;
                break;
            }

            // Gets the difference between the swipe direction and the student direction
            float distance = Vector2.Distance(swipeDirection, studentDirections[i]);

            // Checks to see if the distance is the lowest so far
            if (distance < lowestValue)
            {
                // Sets the current student as closest student
                index = i;
                // Sets the lowest value to the closest student value
                lowestValue = distance;
            }
        }

        // Returns the final closest student index
        return index;
    }

    // Called to redirect the chalk towards a student
    private void RedirectChalk(SwipeData data)
    {
        // If there is a chalk in range
        if (currentChalk)
        {
            // Get the swipe direction
            Vector2 swipeDirection;
            swipeDirection = (data.StartPosition - data.EndPosition).normalized;

            // Rotate chalk to face targeted student
            GameObject closestStudent = students[GetClosestStudentDirectionIndex(swipeDirection)];
            currentChalk.GetComponent<Chalk>().setup((closestStudent.transform.position - currentChalk.transform.position).normalized);
            currentChalk = null;
        }
        else // If there is no chalk in range
        {
            Debug.Log("Missed chalk");
        }    
    }
}
