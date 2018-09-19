using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroInput : AbstractInput
{
    public Quaternion rotation { get; private set; }
    Vector3 move;


    public override void Start()
    {
        //ジャイロ有効化
        Input.gyro.enabled = true;

        //回転値
        rotation = new Quaternion(0, 0, 0, 1);

        move = Vector3.zero;
    }


    // Update is called once per frame
    public override void Update()
    {
        //ジャイロの値取得
        Vector3 rot = Input.gyro.rotationRate;

        //誤差丸め
        //if (Mathf.Abs(r.x) < RoundValue) r.x = 0.0f;
        //if (Mathf.Abs(r.y) < RoundValue) r.y = 0.0f;
        //if (Mathf.Abs(r.z) < RoundValue) r.z = 0.0f;

        rot.x *= -1;
        
        //今までの移動量に加算
        move += rot;

        //端末の傾きを基にオブジェクトの回転具合算出
        rotation = Quaternion.Euler(move);

        //
        Quaternion gyro;
        gyro = Input.gyro.attitude;
        gyro = new Quaternion(-gyro.x, -gyro.z, -gyro.y, gyro.w);
        gyro *= Quaternion.Euler(90, 0, 0);
        rotation = gyro;
    }
}

