using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk : MonoBehaviour
{
    [SerializeField] private float smooth = 5.0f;
    [SerializeField] private float movespeed = -1f;
    private Vector3 shootDir;
    private Quaternion target;
     
    public void setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Rotate the cube by converting the angles into a quaternion.
        target = Quaternion.Euler(-90, this.transform.rotation.y, this.transform.rotation.z);

        
    }

    // Update is called once per frame
    void Update()
    {
        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        transform.position += shootDir * movespeed * Time.deltaTime;
    }
}
