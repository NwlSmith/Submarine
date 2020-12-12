using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaby : MonoBehaviour
{
    public bool Attacking; //If the baby is charging
    public Rigidbody MyRB;

    void Start()
    {
        
    }

    void Update()
    {
        if (Attacking) 
        {
            transform.LookAt(PlayerManager.instance.transform);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Attacking && collision.gameObject.tag == "Player") 
        {
            Debug.Log("Attacked Player");
            MyRB.AddForce(transform.forward * - 300);
            //Call Player take damage method
        }
    }

    private void FixedUpdate()
    {
        if (Attacking)
        {
            MyRB.AddForce(transform.forward * 1);
        }
    }

}
