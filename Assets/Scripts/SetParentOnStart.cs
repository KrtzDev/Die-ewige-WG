using UnityEngine;

public class SetParentOnStart : MonoBehaviour
{
    [SerializeField]
    private Transform parentOnStart;

    private void Start()
    {
        transform.parent = parentOnStart;
    }
}
