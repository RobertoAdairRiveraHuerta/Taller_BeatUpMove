using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;

    void Start()
    {
        // Buscar el componente CinemachineVirtualCamera
        CinemachineVirtualCamera cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        if (cinemachineCamera != null)
        {
            noise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogError("❌ No se encontró CinemachineVirtualCamera en este objeto.");
        }
    }

    public void StartShake(float duration, float amplitude, float frequency)
    {
        if (noise == null)
        {
            Debug.LogError("❌ No se encontró el componente CinemachineBasicMultiChannelPerlin.");
            return;
        }

        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
        shakeTimer = duration;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                // Detener la vibración
                noise.m_AmplitudeGain = 0;
                noise.m_FrequencyGain = 0;
            }
        }
    }
}
