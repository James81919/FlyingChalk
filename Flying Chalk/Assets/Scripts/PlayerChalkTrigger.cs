using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChalkTrigger : MonoBehaviour
{
    private PlayerSwipe playerSwipe;

    private void Awake()
    {
        // Linking to player swipe script
        playerSwipe = GetComponentInParent<PlayerSwipe>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object entering the trigger is chalk...
        if (other.CompareTag("Chalk"))
        {
            // Set current chalk to the new chalk object
            playerSwipe.SetCurrentChalk(other.gameObject);
        }
    }
}
