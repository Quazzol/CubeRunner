using UnityEngine;

public static class Extensions
{
    public static bool AreEqual(this Vector3 first, Vector3 second)
    {
        return Vector3.Distance(first, second) < float.Epsilon;
    }

    public static bool AreEqual(this float first, float second)
    {
        return Mathf.Abs(first - second) < float.Epsilon;
    }
    
    public static void Disable(this IPooledMonoBehaviour pmb)
    {
        pmb.gameObject.SetActive(false);
    }

    public static void DisableAllChildren(this GameObject go)
    {
        Transform transform = go.transform;
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<IPooledMonoBehaviour>(out var pmb))
            {
                pmb.Disable();
            }
        }
    }
}