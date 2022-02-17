using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour {
    public XRNode source;
    public float speed = 1f;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController character;

    void Start() {
        rig = GetComponent<XROrigin>();
        character = GetComponent<CharacterController>();
    }

    void Update() {
        var device = InputDevices.GetDeviceAtXRNode(source);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate() {
        var headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        var direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity
        var isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadset() {
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        var capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded() {
        CapsuleFollowHeadset();
        
        var rayStart = transform.TransformPoint(character.center);
        var rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out var hitInfo, rayLength,
            groundLayer);
        return hasHit;
    }
}