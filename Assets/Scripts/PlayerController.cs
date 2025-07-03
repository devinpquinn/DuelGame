using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float blendSpeed = 2f; // How fast to blend
    private float shieldBlendValue = 0f;
    private float swordBlendValue = 0f;
    private float shieldTarget = 0f;
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

        // Handle up/down arrow keys for Stance_Sword (toggle behavior)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            swordTarget = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            swordTarget = 0f;
        }

        // Continuously blend towards the target values
        shieldBlendValue = Mathf.MoveTowards(shieldBlendValue, shieldTarget, blendSpeed * Time.deltaTime);
        swordBlendValue = Mathf.MoveTowards(swordBlendValue, swordTarget, blendSpeed * Time.deltaTime);

        animator.SetFloat("Stance_Shield", shieldBlendValue);
        animator.SetFloat("Stance_Sword", swordBlendValue);
    }
}
