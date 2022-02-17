using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour {
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedHandModel;

    private Animator handAnimator;
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");

    void Start() {
        CheckFoundDevice();
    }

    void UpdateHandAnimation() {
        if (handAnimator == null)
            return;
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
            handAnimator.SetFloat(Trigger, triggerValue);
        } else {
            handAnimator.SetFloat(Trigger, 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float grip)) {
            handAnimator.SetFloat(Grip, grip);
        } else {
            handAnimator.SetFloat(Grip, 0);
        }
    }

    private void CheckFoundDevice() {
        if (targetDevice.isValid)
            return;
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);

        if (inputDevices.Count > 0) {
            targetDevice = inputDevices[0];
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void Update() {
        UpdateHandAnimation();
        CheckFoundDevice(); 
    }
}