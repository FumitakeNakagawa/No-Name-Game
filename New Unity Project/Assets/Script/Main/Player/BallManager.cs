using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefabs;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Append(float power)
    {
        var ball = Instantiate(ballPrefabs, transform);
        var script = ball.GetComponent<BallScript>();
        script.Create(transform.forward, power);

    }


}
