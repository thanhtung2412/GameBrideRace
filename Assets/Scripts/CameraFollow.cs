using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offet;
    [SerializeField] private float speed = 5f;
    

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offet, speed * Time.deltaTime);
    }
}
