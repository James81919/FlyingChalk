using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teacher : MonoBehaviour
{
    [SerializeField] private Transform chalk;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float min, max;
    [SerializeField] private Transform[] targets;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnChalk", 1f);
    }

    void SpawnChalk()
    {
        //shoot chalk at random students or player
        Transform throwchalk = Instantiate(chalk, firepoint.position,Quaternion.identity);
        Vector3 shootDir = (firepoint.position - targets[0].position).normalized;
        throwchalk.GetComponent<Chalk>().setup(shootDir);

        //random time to call this function again
        float ramTime = Random.Range(min,max);
        Invoke("SpawnChalk", ramTime);
    }
 
}
