using UnityEngine;
using System.Collections;

public class MIRVDeploymentAnimationHandler : MonoBehaviour
{
	void Start ()
    {
        GetComponent<Animator>().speed = 3f;
	}
	
	void Update ()
    {
        AnimatorStateInfo asi = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (asi.normalizedTime >= 1) {
            Destroy(gameObject);
        }
    }
}
