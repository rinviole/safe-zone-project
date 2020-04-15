using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenColor : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeGlowTint(bool changeColor)
    {
        animator.SetBool("glow", changeColor);
    }

    public void DestroyAllAnimation(bool isDestroyAll)
    {
        animator.SetBool("destroyAll", isDestroyAll);
    }
}
