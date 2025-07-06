using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    [Header("Smoothing")]
    [SerializeField] private float swordSmoothingSpeed = 50f;
    [SerializeField] private float shieldSmoothingSpeed = 50f;

    // Private fields
    private Animator animator;
    private Camera playerCamera;
    
    // Target values for smooth interpolation
    private float targetSwordUp = 0f;
    private float targetSwordForward = 0f;
    private float targetShieldUp = 0f;
    
    // Current smoothed values
    private float currentSwordUp = 0f;
    private float currentSwordForward = 0f;
    private float currentShieldUp = 0f;
    
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
        
        // Handle W and S keys for ShieldUp parameter (toggle control)
        if (Input.GetKeyDown(KeyCode.W))
        {
            targetShieldUp = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            targetShieldUp = 0f;
        }
        // Shield maintains target position and smoothly transitions to it
        
        // Smooth interpolation towards target values
        currentSwordForward = Mathf.Lerp(currentSwordForward, targetSwordForward, swordSmoothingSpeed * Time.deltaTime);
        currentSwordUp = Mathf.Lerp(currentSwordUp, targetSwordUp, swordSmoothingSpeed * Time.deltaTime);
        currentShieldUp = Mathf.Lerp(currentShieldUp, targetShieldUp, shieldSmoothingSpeed * Time.deltaTime);
        
        // Set the animator parameters
        animator.SetFloat("SwordForward", currentSwordForward);
        animator.SetFloat("SwordUp", currentSwordUp);
        animator.SetFloat("ShieldUp", currentShieldUp);
    }
    
    /// <summary>
    /// Reset the sword position to center
    /// </summary>
    public void ResetSwordPosition()
    {
        targetSwordUp = 0.5f; // Center of screen vertically
        targetSwordForward = 0.5f; // Center of screen horizontally
        targetShieldUp = 0.5f; // Neutral shield position
    }
    
    /// <summary>
    /// Set custom smoothing speed for parameter transitions
    /// </summary>
    /// <param name="speed">New smoothing speed</param>
    public void SetSmoothingSpeed(float speed)
    {
        swordSmoothingSpeed = Mathf.Max(0.1f, speed);
    }
    
    /// <summary>
    /// Set custom smoothing speed for shield transitions
    /// </summary>
    /// <param name="speed">New shield smoothing speed</param>
    public void SetShieldSmoothingSpeed(float speed)
    {
        shieldSmoothingSpeed = Mathf.Max(0.1f, speed);
    }
    
    /// <summary>
    /// Get current sword parameter values for debugging
    /// </summary>
    /// <returns>Vector2 with (forward, up) values</returns>
    public Vector2 GetCurrentSwordValues()
    {
        return new Vector2(currentSwordForward, currentSwordUp);
    }
    
    /// <summary>
    /// Get current shield parameter value for debugging
    /// </summary>
    /// <returns>Current ShieldUp value</returns>
    public float GetCurrentShieldValue()
    {
        return currentShieldUp;
    }
}
