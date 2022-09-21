using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one Screenshake instance!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cinemachineImpulseSource = (CinemachineImpulseSource)GetComponent("CinemachineImpulseSource");
    }

    public void Shake(float intensity = 1f)
    {
        cinemachineImpulseSource.GenerateImpulse(intensity);
    }


}
