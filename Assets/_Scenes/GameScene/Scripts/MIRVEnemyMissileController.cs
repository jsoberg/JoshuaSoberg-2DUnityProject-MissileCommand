using UnityEngine;
using System.Collections;

public class MIRVEnemyMissileController : EnemyMissileController
{
    protected override bool HasReachedTarget()
    {
        // We want to split the MIRV into smaller missiles at this point.
        return transform.position.y <= GetDeployHeight();
    }

    private int GetDeployHeight()
    {
        switch (Difficulty.GetDifficultyLevel()) {
            case Difficulty.Level.Easy:
                return 20;
            case Difficulty.Level.Normal:
                return 10;
            case Difficulty.Level.Hard:
                return 0;
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    private bool MirvDestroyedByFriendlyMissile;

    protected override int GetScoreAddedForMissileDestruction()
    {
        // This MIRV was destroyed by a friendly missile, so don't deploy when we explode.
        MirvDestroyedByFriendlyMissile = true;
        switch (Difficulty.GetDifficultyLevel())
        {
            case Difficulty.Level.Easy:
                return 300;
            case Difficulty.Level.Normal:
                return 450;
            case Difficulty.Level.Hard:
                return 600;
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    protected override void Explode()
    {
        if (!MirvDestroyedByFriendlyMissile) {
            EnemyAiController controller = (EnemyAiController)Object.FindObjectOfType(typeof(EnemyAiController));
            controller.RapidDeployMissilesFromPosition(4, transform.position);
        }

        base.Explode();
    }
}
