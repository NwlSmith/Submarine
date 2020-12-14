using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCordToStatue : MonoBehaviour
{
    public IKBasic TailScript;
    public EnemyStatue MyStatue;

    void Start()
    {
        MyStatue = transform.parent.GetComponent<EnemyStatue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyStatue != null) 
        {
            TailScript.pole = MyStatue.transform; 
        }
        if (MyStatue != null)
        {
            TailScript.target = MyStatue.MyBaby.transform;
        }

    }
}
