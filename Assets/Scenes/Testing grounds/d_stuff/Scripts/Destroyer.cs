using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject target;

    public float destroyDelay = 0f;

    public void DestroyTarget()
    {
        if (target != null)
        {
            if (destroyDelay > 0f)
            {
                Destroy(target, destroyDelay);
            }
            else
            {
                Destroy(target);
            }
        }
        else
        {
            Debug.LogWarning("not assigned");
        }
    }
}
