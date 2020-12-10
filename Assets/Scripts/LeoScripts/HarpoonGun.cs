using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    public static HarpoonGun instance;
    public GameObject LockedTarget;
    public Rigidbody RB;

    public GameObject HarpoonPrefab;
    public GameObject Harpoon;
    public GameObject RopePrefab;
    public GameObject Rope;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Harpoon == null)
            {
                Instantiate(HarpoonPrefab, transform.position, transform.rotation);
            }
            if (Rope == null) 
            {
                Instantiate(RopePrefab, transform.position, transform.rotation);
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            Destroy(Harpoon);
            Destroy(Rope);
        }

    }
}
