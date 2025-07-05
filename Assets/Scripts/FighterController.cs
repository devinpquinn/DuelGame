using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2f;
    
    [Header("Smoothing")]
    [SerializeField] private float smoothingSpeed = 5f;
    
    [Header("Parameter Ranges")]
    private float maxSwordUp = 1f;
    private float maxSwordForward = 1f;

    // Private fields
    private Animator animator;
    private Camera playerCamera;
    
    // Target values for smooth interpolation
    private float targetSwordUp = 0f;
    private float targetSwordForward = 0f;
    
    // Current smoothed values
    private float currentSwordUp = 0f;
    private float currentSwordForward = 0f;
    
    // Screen center reference
    private Vector2 screenCenter;
    
    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("FighterController: No Animator component found!");
            return;
        }
        
        // Get the main camera or find one tagged as "MainCamera"
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            playerCamera = FindObjectOfType<Camera>();
        }
        
        if (playerCamera == null)
        {
            Debug.LogError("FighterController: No camera found!");
            return;
        }
        
        // Calculate screen center
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        if (animator == null || playerCamera == null) return;
        
        // Get mouse position and calculate relative position from screen center
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseOffset = mousePosition - screenCenter;
        
        // Normalize mouse offset to screen dimensions
        Vector2 normalizedOffset = new Vector2(
            mouseOffset.x / (Screen.width * 0.5f),
            mouseOffset.y / (Screen.height * 0.5f)
        );
        
        // Apply sensitivity and clamp to parameter ranges
        targetSwordForward = Mathf.Clamp(normalizedOffset.x * mouseSensitivity, -maxSwordForward, maxSwordForward);
        targetSwordUp = Mathf.Clamp(normalizedOffset.y * mouseSensitivity, -maxSwordUp, maxSwordUp);
        
        // Smooth interpolation towards target values
        currentSwordForward = Mathf.Lerp(currentSwordForward, targetSwordForward, smoothingSpeed * Time.deltaTime);
        currentSwordUp = Mathf.Lerp(currentSwordUp, targetSwordUp, smoothingSpeed * Time.deltaTime);
        
        // Set the animator parameters
        animator.SetFloat("SwordForward", currentSwordForward);
        animator.SetFloat("SwordUp", currentSwordUp);
    }
    
    /// <summary>
    /// Reset the sword position to center
    /// </summary>
    public void ResetSwordPosition()
    {
        targetSwordUp = 0f;
        targetSwordForward = 0f;
    }
    
    /// <summary>
    /// Set custom sensitivity for mouse input
    /// </summary>
    /// <param name="sensitivity">New sensitivity value</param>
    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = Mathf.Max(0.1f, sensitivity);
    }
    
    /// <summary>
    /// Set custom smoothing speed for parameter transitions
    /// </summary>
    /// <param name="speed">New smoothing speed</param>
    public void SetSmoothingSpeed(float speed)
    {
        smoothingSpeed = Mathf.Max(0.1f, speed);
    }
    
    /// <summary>
    /// Get current sword parameter values for debugging
    /// </summary>
    /// <returns>Vector2 with (forward, up) values</returns>
    public Vector2 GetCurrentSwordValues()
    {
        return new Vector2(currentSwordForward, currentSwordUp);
    }
}
