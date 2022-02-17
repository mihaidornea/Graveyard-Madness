using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementController : MonoBehaviour {

    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button activateButton;
    public float activationThreshold = 0.1f;

    void Update()
    {
        if (leftTeleportRay) {
            leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay));
        } 
        if (rightTeleportRay) {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }
    }

    public bool CheckIfActivated(XRController controller) {
        controller.inputDevice.IsPressed(activateButton, out var isActivated, activationThreshold);
        return isActivated;
    }
}
