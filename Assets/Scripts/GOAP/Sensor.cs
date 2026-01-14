using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float timeInterval = 1f;

    SphereCollider detectionRange;

    public event Action OnTargetChanged;

    private GameObject target;

    public Vector3 TargetPosition => target != null ? target.transform.position : Vector3.zero;
    public bool IsTargetInRange => TargetPosition != Vector3.zero;

    private Vector3 lastKnownPosition;
    private float t = 0;

    private void Awake()
    {
        detectionRange = GetComponent<SphereCollider>();
        detectionRange.isTrigger = true;
        detectionRange.radius = detectionRadius;
    }

    private void Start()
    {
        t = timeInterval;
    }
}
