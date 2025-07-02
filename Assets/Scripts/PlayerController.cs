using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float blendSpeed = 2f; // How fast to blend
    private float blendValue = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            blendValue = Mathf.MoveTowards(blendValue, 1f, blendSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            blendValue = Mathf.MoveTowards(blendValue, 0f, blendSpeed * Time.deltaTime);
        }

        animator.SetFloat("Stance", blendValue);
    }
}
