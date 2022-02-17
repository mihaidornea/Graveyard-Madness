using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HealthController : MonoBehaviour
{
    public XRController leftController;
    public InputHelpers.Button activateButton;
    public float activationThreshold = 0.1f;
    public Image health;

    void Update()
    {
        if (leftController) {
            health.gameObject.SetActive(CheckIfActivated(leftController));
        } 
    }

    public bool CheckIfActivated(XRController controller) {
        controller.inputDevice.IsPressed(activateButton, out var isActivated, activationThreshold);
        return isActivated;
    }
}
