using UnityEngine;

public class canvasfixrotation : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localRotation = Quaternion.identity;
    }
}