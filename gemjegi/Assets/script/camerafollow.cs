using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("따라갈 타겟 (예: Car)")]
    public Transform target;

    [Header("Follow Settings")]
    [Tooltip("카메라가 따라갈 때의 부드러움 정도")]
    public float smoothSpeed = 0.125f;

    [Tooltip("카메라 오프셋 (예: 약간 앞쪽을 보게 하고 싶을 때)")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
