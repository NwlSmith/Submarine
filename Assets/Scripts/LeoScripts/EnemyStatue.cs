using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatue : MonoBehaviour
{
    public EnemyBaby MyBaby;
    public GameObject BabyPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.transform) 
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && MyBaby != null) 
        {
            MyBaby.Attacking = true;
        }
    }

    public void SpawnBaby() 
    {
        Instantiate(BabyPrefab, this.transform);
        
    }
}
