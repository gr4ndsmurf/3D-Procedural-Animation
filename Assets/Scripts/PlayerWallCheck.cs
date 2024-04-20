using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWallCheck : MonoBehaviour
{
    [SerializeField] private Transform shoulderDirection, leftHandTarget;
    [SerializeField] private float checkDistance, leftHandTargetPositionOffset;
    [SerializeField] private LayerMask checkLayers;
    [SerializeField] private Vector3 leftHandTargetRotationOffset;
    [SerializeField] private TwoBoneIKConstraint leftHandIK;

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
            leftHandTarget.position = hit.point - direction * leftHandTargetPositionOffset;
            leftHandTarget.eulerAngles = Quaternion.LookRotation(hit.normal).eulerAngles + leftHandTargetRotationOffset;
            targetWeight = 1f;
        }

        leftHandIK.weight = Mathf.Lerp(leftHandIK.weight, targetWeight, Time.deltaTime * 3f);
    }
}
