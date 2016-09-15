using UnityEngine;
using System.Collections;

public class DestroyExplostionWhenComplete : MonoBehaviour 
{
    public Animator ExplosionAnimator;
    	
    void Start()
    {
        ExplosionAnimator = ExplosionAnimator.GetComponent<Animator>();
    }

	void Update () 
    {
        AnimatorStateInfo asi = ExplosionAnimator.GetCurrentAnimatorStateInfo(0);

        if (asi.normalizedTime >= 1) {
            Destroy(gameObject);
        }
	}
}
