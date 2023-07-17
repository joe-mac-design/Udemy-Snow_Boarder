using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailCondition : MonoBehaviour
{

    [SerializeField] float _sceneRestartDelay = 0.5f;
    [SerializeField] ParticleSystem _failParticles;

    void OnTriggerEnter2D (Collider2D other)
    {
        // If the player hits the ground, restart the scene.
        if(other.tag == "Ground") 
        {
            // Play the fail particles.
            _failParticles.Play();
            // Invoke a Restart of the scene.
            Invoke("RestartScene", _sceneRestartDelay);
        }
    }

    void RestartScene()
    {
        // Restart scene 0.
        SceneManager.LoadScene(0);
    }
}
