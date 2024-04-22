using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWallCheck : MonoBehaviour
{
    [SerializeField] private Transform shoulderDirection, leftHandTarget, leftHandHint;
    [SerializeField] private float checkDistance, leftHandTargetPositionOffset;
    [SerializeField] private LayerMask checkLayers;
    [SerializeField] private Vector3 leftHandTargetRotationOffset;
    [SerializeField] private TwoBoneIKConstraint leftHandIK;

    [SerializeField] private float IK_Weight_LerpTime;

    private void Start()
    {
        leftHandIK.weight = 0f;
    }
    private void Update()
    {
        Vector3 startPoint = shoulderDirection.position;
        Vector3 direction = -shoulderDirection.right;

        Debug.DrawRay(startPoint, direction * checkDistance, Color.red);

        float targetWeight = 0f;
        if (Physics.Raycast(startPoint,direction,out RaycastHit hit, checkDistance, checkLayers))
        {
            // Çarpýþma noktasý ve normal vektörünü al
            Vector3 hitPoint = hit.point;

            // Elin hedef konumunu belirle
            Vector3 targetPosition = hitPoint - direction * leftHandTargetPositionOffset;

            // Elin hedef rotasyonunu belirle
            Vector3 relativePos = hitPoint - leftHandTarget.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos.normalized, Vector3.up) * Quaternion.Euler(leftHandTargetRotationOffset.x, leftHandTargetRotationOffset.y, leftHandTargetRotationOffset.z);

            // Elin pozisyonunu ve rotasyonunu güncelle
            leftHandTarget.position = targetPosition;
            leftHandTarget.rotation = targetRotation;
            leftHandHint.rotation = targetRotation;

            targetWeight = 1f;
        }

        leftHandIK.weight = Mathf.Lerp(leftHandIK.weight, targetWeight, Time.deltaTime * IK_Weight_LerpTime);
    }
}
