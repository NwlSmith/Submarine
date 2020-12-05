using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public GameObject Rope;
    public GameObject Child;
    public Rigidbody RB;
    public SpringJoint MyJoint;

    public float Force;

    void Awake() 
    {
        MyJoint.connectedBody = HarpoonGun.instance.RB;
    }
    void Start()
    {

        transform.position = HarpoonGun.instance.gameObject.transform.position;
        transform.rotation = HarpoonGun.instance.gameObject.transform.rotation;
        RB.AddForce(transform.forward*Force);
    }

    // Update is called once per frame
    void Update()
    {
        // Rope.transform.position = HarpoonGun.instance.gameObject.transform.position;
        if (true) 
        {
        
        }
    }

    void Delete() 
    {
        Child.transform.parent = null;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        {
        }
    }

}
