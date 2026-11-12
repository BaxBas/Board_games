using UnityEngine;

public class DiceScript : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void ShakeDice()
    {
        animator.SetBool("isRolling", true);
    }

    public void StopShakeDice()
    {
        animator.SetBool("isRolling", false);
    }
}
