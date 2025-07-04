using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float blendSpeed = 2f; // How fast to blend
    public float mouseThreshold = 0.1f; // Minimum mouse movement to trigger sword movement
    private float shieldBlendValue = 0f;
    private float shieldTarget = 0f;
    private float shieldBlockBlendValue = 0f;
    private float shieldBlockTarget = 0f;
    private float swordBlendValue = 0f;
    private float swordTarget = 0f;

    void Update()
    {
        // Handle W/S keys for Stance_Shield (toggle behavior)
        if (Input.GetKeyDown(KeyCode.W))
        {
            shieldTarget = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            shieldTarget = 0f;
        }
        
        // Handle mouse movement for Sword_Up
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseY > mouseThreshold)
        {
            swordTarget = 1f; // Moving up
        }
        else if (mouseY < -mouseThreshold)
        {
            swordTarget = 0f; // Moving down
        }
        
        if(Input.GetMouseButton(1))
        {
            // If held, set the block target
            shieldBlockTarget = 1f;
        }
        else
        {
            // If released, reset the block target
            shieldBlockTarget = 0f;
        }

        // Continuously blend towards the target values
        shieldBlendValue = Mathf.MoveTowards(shieldBlendValue, shieldTarget, blendSpeed * Time.deltaTime);
        shieldBlockBlendValue = Mathf.MoveTowards(shieldBlockBlendValue, shieldBlockTarget, blendSpeed * Time.deltaTime);
        swordBlendValue = Mathf.MoveTowards(swordBlendValue, swordTarget, blendSpeed * Time.deltaTime);

        animator.SetFloat("Shield_Up", shieldBlendValue);
        animator.SetFloat("Shield_Block", shieldBlockBlendValue);
        animator.SetFloat("Sword_Up", swordBlendValue);
    }
}
