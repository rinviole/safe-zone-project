using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        animator.SetBool("shake", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("shake", false);
    }

}
