using UnityEngine;

public class DestroyWhenAnimationFinished : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
   
}
