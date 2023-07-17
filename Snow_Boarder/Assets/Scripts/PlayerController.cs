using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{

    [SerializeField] float _torqueAmount = 1f;
    [SerializeField] float _baseSpeed = 20f;
    [SerializeField] float _boostSpeed = 25f;
    [SerializeField] float _boostDuration = 1f;
    [SerializeField] float _boostCooldown = 3f;
    Rigidbody2D _rigidbody2d;
    SurfaceEffector2D _surfaceEffector2d;
    private bool _canBoost = true;
    private bool _isBoosting = false;
    private float _boostEndTime = 0f;

    public TMP_Text boostDurationText;
    public TMP_Text boostCooldownText;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _surfaceEffector2d = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();

        if (_canBoost && (Input.GetKeyDown(KeyCode.LeftShift)))
        {
            BoostPlayer();
        }

        UpdateUI();
    }

    private void BoostPlayer()
    {
        Debug.Log("Boost triggered");

        _isBoosting = true;
        _boostEndTime = Time.time + _boostDuration;

        // Set the surface speed to the boost speed
        _surfaceEffector2d.speed = _boostSpeed;

        // After the boost duration has passed, revert the speed back to the base speed
        StartCoroutine(RevertSpeedAfterDelay(_boostDuration));

        // After the boost cooldown has passed, enable the player to boost again
        StartCoroutine(EnableBoostAfterDelay(_boostCooldown));
    }

    private IEnumerator RevertSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _surfaceEffector2d.speed = _baseSpeed;
        Debug.Log("Boost ended");
        _isBoosting = false;
    }

    private IEnumerator EnableBoostAfterDelay(float delay)
    {
        _canBoost = false;
        Debug.Log("Boost on cooldown");
        yield return new WaitForSeconds(delay);
        _canBoost = true;
        Debug.Log("Boost available again");
    }

    private void UpdateUI()
    {
        if (_isBoosting)
        {
            float remainingTime = Mathf.Max(0f, _boostEndTime - Time.time);
            boostDurationText.text = "Boost Duration: " + remainingTime.ToString("F1") + "s";
        }
        else
        {
            boostDurationText.text = "Boost Duration: 0s";
        }

        if (!_canBoost)
        {
            float remainingTime = Mathf.Max(0f, _boostEndTime + _boostCooldown - Time.time);
            boostCooldownText.text = "Boost Cooldown: " + remainingTime.ToString("F1") + "s";
            float cooldownProgress = 1f - (remainingTime / _boostCooldown);
        }
        else
        {
            boostCooldownText.text = "Boost Cooldown: 0s";
        }
    }

    private void RotatePlayer()
    {
        // If the player presses the A key, add torque to the rigidbody. If the player presses the D key, add negative torque to the rigidbody.
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody2d.AddTorque(_torqueAmount);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2d.AddTorque(-_torqueAmount);
        }
    }
}
