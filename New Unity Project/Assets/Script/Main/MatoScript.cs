using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatoScript : MonoBehaviour
{
    [SerializeField]
    GameObject particlePrefab;

    MatoManager matoManager;


    // Use this for initialization
    void Start()
    {
        matoManager = transform.parent.GetComponent<MatoManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        matoManager.CountDown();
        var particle = Instantiate(particlePrefab, transform.position, new Quaternion(0, 0, 0, 1));
        Destroy(gameObject);
    }


}
