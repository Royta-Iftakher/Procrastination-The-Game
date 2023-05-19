using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public Animator characterAnimator;  // Reference to the Animator component of your character.
    public string animationName = "AnimationClipName"; // Name of the animation clip to play.

    // This function is automatically called by Unity when another collider enters the TV's trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the character.
        // This assumes the character has a tag "Character".
        if (other.CompareTag("Player"))
        {
            // If it's the character, play the animation.
            characterAnimator.Play(animationName);
        }
    }
}
