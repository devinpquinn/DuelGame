using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float blendSpeed = 2f; // How fast to blend
    private float shieldBlendValue = 0f;
    private float shieldTarget = 0f;
    private float shieldBlockBlendValue = 0f;
    private float shieldBlockTarget = 0f;

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

        animator.SetFloat("Shield_Up", shieldBlendValue);
        animator.SetFloat("Shield_Block", shieldBlockBlendValue);
    }
}
