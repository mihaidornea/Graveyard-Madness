using System;
using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;

public class SwordGrabbable : DistanceGrabbable
{
    private void Update() {
        if (m_grabbedBy != null) {
            transform.rotation = m_grabbedBy.transform.rotation;
        }
    }
}
