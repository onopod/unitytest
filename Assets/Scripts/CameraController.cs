using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(3, 2, 3);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(
                transform.position,
                targetCamPos,
                Time.deltaTime * smoothing
            );
            transform.LookAt(target.transform);
        }

    }
}
