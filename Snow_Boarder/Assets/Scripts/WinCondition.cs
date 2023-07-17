using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{

    [SerializeField] float _sceneRestartDelay = 0.5f;
    [SerializeField] ParticleSystem _winParticles;

    void OnTriggerEnter2D (Collider2D other)
    {
        // If the player reaches the finish line, they win.
        if(other.tag == "Player") 
        {
            // Play the win particles.
            _winParticles.Play();
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
