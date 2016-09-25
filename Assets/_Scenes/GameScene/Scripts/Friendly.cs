using UnityEngine;
using System.Collections;

// This is simply a class that will be used to signify is a GameObject is a friendly or not.
public class Friendly : MonoBehaviour
{
    public static bool IsFriendly(GameObject obj)
    {
        return obj.GetComponent<Friendly>() != null;
    }
}
