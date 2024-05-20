using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EarthquakeEffect : MonoBehaviour
{
    public static event Action AfterEartquakeEffect;


    public float shakeDuration = 2.0f;
    public float shakeMagnitude = 0.05f;
    public float shakeSpeed = 1.0f;
    private bool isShaking = false;
    private float shakeTime = 0.0f;

    public ParticleSystem dustEffect;

    private void Awake()
    {
        AfterEartquakeEffect = null;
    }

    private void Start()
    {
        CountdownTimer.FinishTimer += StartEarthquake;
    }

    void Update()
    {
        if (isShaking)
        {
            if (shakeTime < shakeDuration)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
                transform.position += shakeOffset;
                transform.rotation = Quaternion.Euler(Random.insideUnitSphere * shakeMagnitude) * transform.rotation;
                shakeTime += Time.deltaTime * shakeSpeed;

                TriggerDestruction();

            }
            else
            {
                AfterEartquakeEffect?.Invoke();
                isShaking = false;
                //TriggerDestruction();
            }
        }
    }

    public void StartEarthquake()
    {
        isShaking = true;
        shakeTime = 0.0f;
        dustEffect.Play();
    }

    private void TriggerDestruction()
    {
        Debug.Log("Triggering destruction");
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false; // Parçalarý serbest býrak
        }
    }
}
