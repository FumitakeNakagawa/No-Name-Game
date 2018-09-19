using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Android
{
    class InputManager
    {
        //ジャイロセンサー
        public GyroInput gyroInput { get; private set; }

        //タッチ入力
        public TouchInput touchInput { get; private set; }

        public void Start()
        {
            //ジャイロ入力インスタンス生成・初期化
            gyroInput = new GyroInput();
            gyroInput.Start();

            //タッチ入力インスタンス生成・初期化
            touchInput = new TouchInput();
            touchInput.Start();

        }


        public void Update()
        {
            gyroInput.Update();
            touchInput.Update();
        }
    }
}
