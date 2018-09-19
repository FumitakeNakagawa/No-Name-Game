using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private InputManagerScript inputManagerScript;

    private BallManager ballManager;

    [SerializeField]
    private float minPower, maxPower;

    [SerializeField, Header("チャージマックスまでの時間")]
    public float maxStoreTime;
    
    private float power;

    private float powerRate;
    private float framePowerRate;

    private float chargeTime;

    void Start()
    {
        ballManager = GetComponent<BallManager>();
        power = 0;

        //秒間の増加量計算
        powerRate = maxPower / maxStoreTime;
        
        //フレーム間の増加量計算
        framePowerRate = powerRate * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
    }


    void UpdateRotation()
    {
#if UNITY_STANDALONE || UNITY_EDITOR

        //transform.rotation = inputManagerScript.inputManager.keyInput.rotation;
        transform.rotation = Quaternion.Euler(inputManagerScript.inputManager.mouseInput.total);

#elif UNITY_ANDROID
        transform.rotation = inputManagerScript.inputManager.gyroInput.rotation;
#endif
    }

    //力をためる
    public void StoringTheForce()
    {
        power += framePowerRate;
        power = Mathf.Max(minPower, power);
        power = Mathf.Min(maxPower, power);
    }

    //放つ
    public void Release()
    {
        ballManager.Append(power);
        power = 0;
    }
}
