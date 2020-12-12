using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotSpeed;

    private void FixedUpdate()
    {
        transform.localEulerAngles += rotSpeed * Time.fixedDeltaTime;
    }
}
