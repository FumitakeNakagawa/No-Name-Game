using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    [SerializeField]
    private InputManagerScript inputManagerScript;

    private new MeshCollider  collider ;

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_STANDALONE ||UNITY_EDITOR

        if (inputManagerScript.inputManager.mouseInput.mouseWheel > .0f)
        {
            transform.localScale *= 1.2f;
            collider.transform.localScale *= 1.2f;
        }
        else if (inputManagerScript.inputManager.mouseInput.mouseWheel < .0f)
        {
            transform.localScale *= 0.8f;
            collider.transform.localScale *= 0.8f;
        }

#elif UNITY_ANDROID
        transform.localScale *= (1+inputManagerScript.inputManager.touchInput.pinchRatio);


#endif
    }
}
