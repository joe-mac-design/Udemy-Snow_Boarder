using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem _trailParticleSystem;

    void OnCollisionEnter2D(Collision2D other)
    {
        // If the player hits the ground, start the particle trail.
        if(other.gameObject.tag == "Ground") 
        {
            _trailParticleSystem.Play();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // If the player leaves the ground, stop the particle trail after a short delay.
        if(other.gameObject.tag == "Ground") 
        {
            StartCoroutine(StopTrailAfterDelay(1.25f));
        }
    }

    private IEnumerator StopTrailAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _trailParticleSystem.Stop();
    }
}