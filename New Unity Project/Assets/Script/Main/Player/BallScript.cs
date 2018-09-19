using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private Vector3 forceVec;
    private float forcePower;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        transform.parent = null;
        var force = forceVec * forcePower;
        rigidbody.AddForce(force);
    }

    public void Create(Vector3 forceVector, float power)
    {
        //力の強さと向きを受け取り
        this.forceVec = forceVector;
        this.forcePower = power;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
