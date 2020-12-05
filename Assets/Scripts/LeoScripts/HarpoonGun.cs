using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    public static HarpoonGun instance;
    public GameObject LockedTarget;
    public Rigidbody RB;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
