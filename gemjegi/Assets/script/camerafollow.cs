using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("���� Ÿ�� (��: Car)")]
    public Transform target;

    [Header("Follow Settings")]
    [Tooltip("ī�޶� ���� ���� �ε巯�� ����")]
    public float smoothSpeed = 0.125f;

    [Tooltip("ī�޶� ������ (��: �ణ ������ ���� �ϰ� ���� ��)")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
