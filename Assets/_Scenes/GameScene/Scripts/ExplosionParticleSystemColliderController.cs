using UnityEngine;
using System.Collections;

public class ExplosionParticleSystemColliderController : MonoBehaviour
{
    private const float RADIUS_FACTOR = .6f;

    public ParticleSystem ExplosionParticleSystem;
    public CircleCollider2D CircleCollider;

    private ParticleSystem.Particle[] ReuseParticles = new ParticleSystem.Particle[1000];

    void Update ()
    {
        if (CheckForDeath()) {
            return;
        }

        float height = GetHeightOfParticleSystem();
        CircleCollider.radius = height;
	}

    private bool CheckForDeath()
    {
        if (!ExplosionParticleSystem.IsAlive()) {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    private float GetHeightOfParticleSystem()
    {
        int numParticles = ExplosionParticleSystem.GetParticles(ReuseParticles);
        float maxHeight = float.MinValue;
        float minHeight = float.MaxValue;
        for (int i = 0; i < numParticles; i ++)
        {
            float height = ReuseParticles[i].position.y;
            if (height > maxHeight) {
                maxHeight = height;
            } if (height < minHeight) {
                minHeight = height;
            }
        }

        return (maxHeight - minHeight) * RADIUS_FACTOR;
    }
}
