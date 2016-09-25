using UnityEngine;
using System.Collections;

// This is simply a class that will be used to signify is a GameObject is an enemy or not.
public class Enemy : MonoBehaviour
{
    public static bool IsEnemy(GameObject obj)
    {
        return obj.GetComponent<Enemy>() != null;
    }
}
