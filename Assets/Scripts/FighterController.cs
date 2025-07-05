using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
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
    }

    void Update()
    {
        if (animator == null || playerCamera == null) return;
        
        // Get mouse position
        Vector2 mousePosition = Input.mousePosition;
        
        // Calculate SwordUp based on vertical mouse position (0 at bottom, 1 at top)
        targetSwordUp = Mathf.Clamp01(mousePosition.y / Screen.height);
        
        // Calculate SwordForward based on horizontal mouse position (0 at left, 1 at right)
        targetSwordForward = Mathf.Clamp01(mousePosition.x / Screen.width);
        
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
        targetSwordUp = 0.5f; // Center of screen vertically
        targetSwordForward = 0.5f; // Center of screen horizontally
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
