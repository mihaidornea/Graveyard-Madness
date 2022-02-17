using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour {
    
    private DeviceBasedContinuousMoveProvider _provider;
    private CharacterController _controller;

    public static XRController climbingHand;
    
    void Start() {
        _provider = GetComponent<DeviceBasedContinuousMoveProvider>();
        _controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        if (climbingHand) {
            _provider.enabled = false;
            Climb();
        } else {
            _provider.enabled = true;
        }
    }

    private void Climb() {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode)
            .TryGetFeatureValue(CommonUsages.deviceVelocity, out var velocity);
        _controller.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
