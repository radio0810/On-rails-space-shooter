using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the player moves up and down")]
    [SerializeField] float controlSpeed = 30f;
    [Header("Axis Limiter")]
    [Tooltip("The range the ship moves on the x axis")]
    [SerializeField] float xRange = 10f;
    [Tooltip("The lower limit on the y axis")]
    [SerializeField] float yNegativeRange = -3f;
    [Tooltip("The upper limit on the y axis")]
    [SerializeField] float yPostiveRange = 11f;
    [Header("Laser Array")]
    [SerializeField] GameObject[] lasers; 

    [Header("Movement controls")]
    [Tooltip("Controls nose up and down depending on buttons pressed")]
    [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("Controls the how much pitch up and down")]
    [SerializeField] float controlPitchFactor = -10f;
    [Tooltip("Controls the pitch of the ship horiozontally")]
    [SerializeField] float positionYawFactor = 2f;
    [Tooltip("Controls the amount of the roll of the shipC")]
    [SerializeField] float positionRollFactor = 10f;
    float xThrow;
    float yThrow;



    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch =  pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x *positionYawFactor;
        float roll = xThrow * positionRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");

        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;


        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, +xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, yNegativeRange, yPostiveRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
            
        }
    }

   
}
