using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float blendSpeed = 2f; // How fast to blend
    private float shieldBlendValue = 0f;
    private float swordBlendValue = 0f;

    void Update()
    {
        // Handle W/S keys for Stance_Shield
        bool wPressed = Input.GetKey(KeyCode.W);
        bool sPressed = Input.GetKey(KeyCode.S);

        // Only blend if exactly one key is pressed (horizontal axis behavior)
        if (wPressed && !sPressed)
        {
            shieldBlendValue = Mathf.MoveTowards(shieldBlendValue, 1f, blendSpeed * Time.deltaTime);
        }
        else if (sPressed && !wPressed)
        {
            shieldBlendValue = Mathf.MoveTowards(shieldBlendValue, 0f, blendSpeed * Time.deltaTime);
        }
        // If both keys are pressed or neither is pressed, shieldBlendValue remains unchanged

        // Handle up/down arrow keys for Stance_Sword
        bool upPressed = Input.GetKey(KeyCode.UpArrow);
        bool downPressed = Input.GetKey(KeyCode.DownArrow);

        // Only blend if exactly one key is pressed (horizontal axis behavior)
        if (upPressed && !downPressed)
        {
            swordBlendValue = Mathf.MoveTowards(swordBlendValue, 1f, blendSpeed * Time.deltaTime);
        }
        else if (downPressed && !upPressed)
        {
            swordBlendValue = Mathf.MoveTowards(swordBlendValue, 0f, blendSpeed * Time.deltaTime);
        }
        // If both keys are pressed or neither is pressed, swordBlendValue remains unchanged

        animator.SetFloat("Stance_Shield", shieldBlendValue);
        animator.SetFloat("Stance_Sword", swordBlendValue);
    }
}
