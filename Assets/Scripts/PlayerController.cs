using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float blendSpeed = 2f; // How fast to blend
    private float blendValue = 0f;

    void Update()
    {
        bool wPressed = Input.GetKey(KeyCode.W);
        bool sPressed = Input.GetKey(KeyCode.S);

        // Only blend if exactly one key is pressed (horizontal axis behavior)
        if (wPressed && !sPressed)
        {
            blendValue = Mathf.MoveTowards(blendValue, 1f, blendSpeed * Time.deltaTime);
        }
        else if (sPressed && !wPressed)
        {
            blendValue = Mathf.MoveTowards(blendValue, 0f, blendSpeed * Time.deltaTime);
        }
        // If both keys are pressed or neither is pressed, blendValue remains unchanged

        animator.SetFloat("Stance_Shield", blendValue);
    }
}
