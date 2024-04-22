using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWallCheck : MonoBehaviour
{
    [Header("Left Hand")]
    [SerializeField] private Transform leftShoulderDirection;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform leftHandHint;
    [SerializeField] private float leftHandTargetPositionOffset;
    [SerializeField] private Vector3 leftHandTargetRotationOffset;
    [SerializeField] private TwoBoneIKConstraint leftHandIK;

    [Header("Right Hand")]
    [SerializeField] private Transform rightShoulderDirection;
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform rightHandHint;
    [SerializeField] private float rightHandTargetPositionOffset;
    [SerializeField] private Vector3 rightHandTargetRotationOffset;
    [SerializeField] private TwoBoneIKConstraint rightHandIK;

    [Header("---")]
    [SerializeField] private float IK_Weight_LerpTime;
    [SerializeField] private LayerMask checkLayers;
    [SerializeField] private float checkDistance;

    private void Start()
    {
        leftHandIK.weight = 0f;
        rightHandIK.weight = 0f;
    }
    private void Update()
    {
        #region LEFT HAND
        Vector3 leftStartPoint = leftShoulderDirection.position;
        Vector3 leftDirection = -leftShoulderDirection.right;

        Debug.DrawRay(leftStartPoint, leftDirection * checkDistance, Color.red);

        float leftTargetWeight = 0f;
        if (Physics.Raycast(leftStartPoint, leftDirection, out RaycastHit hit, checkDistance, checkLayers))
        {
            // Elin hedef konumunu belirle
            Vector3 leftTargetPosition = hit.point - leftDirection * leftHandTargetPositionOffset;

            // Elin hedef rotasyonunu belirle
            Quaternion leftTargetRotation = Quaternion.LookRotation(hit.normal.normalized) * Quaternion.Euler(leftHandTargetRotationOffset.x, leftHandTargetRotationOffset.y, leftHandTargetRotationOffset.z);

            // Elin pozisyonunu ve rotasyonunu güncelle
            leftHandTarget.position = leftTargetPosition;
            leftHandTarget.rotation = leftTargetRotation;
            leftHandHint.rotation = leftTargetRotation;

            leftTargetWeight = 1f;
        }

        leftHandIK.weight = Mathf.Lerp(leftHandIK.weight, leftTargetWeight, Time.deltaTime * IK_Weight_LerpTime);
        #endregion

        #region RIGHT HAND
        Vector3 rightStartPoint = rightShoulderDirection.position;
        Vector3 rightDirection = rightShoulderDirection.right;

        Debug.DrawRay(rightStartPoint, rightDirection * checkDistance, Color.red);

        float rightTargetWeight = 0f;
        if (Physics.Raycast(rightStartPoint, rightDirection, out RaycastHit hit2, checkDistance, checkLayers))
        {
            // Elin hedef konumunu belirle
            Vector3 rightTargetPosition = hit2.point - rightDirection * rightHandTargetPositionOffset;

            // Elin hedef rotasyonunu belirle
            Quaternion rightTargetRotation = Quaternion.LookRotation(hit2.normal.normalized) * Quaternion.Euler(rightHandTargetRotationOffset.x, rightHandTargetRotationOffset.y, rightHandTargetRotationOffset.z);

            // Elin pozisyonunu ve rotasyonunu güncelle
            rightHandTarget.position = rightTargetPosition;
            rightHandTarget.rotation = rightTargetRotation;
            rightHandHint.rotation = rightTargetRotation;

            rightTargetWeight = 1f;
        }

        rightHandIK.weight = Mathf.Lerp(rightHandIK.weight, rightTargetWeight, Time.deltaTime * IK_Weight_LerpTime);
        #endregion


    }

}
