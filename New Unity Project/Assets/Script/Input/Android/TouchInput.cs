using UnityEngine;

namespace Android
{
    public class TouchInput : AbstractInput
    {
        //タッチ開始位置
        private Vector2 touchStartPos;

        //タッチ終了位置
        private Vector2 touchEndPos;

        //スワイプ方向のベクトル
        private Vector2 swipeVec;

        //2点間の距離
        private float pinchDist;

        //2点間の初期距離
        private float startPinchDist;

        private float moveDist;

        //ピンチした割合
        public float pinchRatio { get; private set; }

        public static float radius;

        // Use this for initialization
        public override void Start()
        {

            touchStartPos = Vector2.zero;
            touchEndPos = Vector2.zero;
            pinchDist = -1f;
            pinchRatio = 0.0f;
        }

        // Update is called once per frame
        public override void Update()
        {
            //タッチしている指の本数を全取得
            Touch[] touches = Input.touches;

            //指が触れていなければ
            if (touches.Length <= 0)
                return;

            //シングルタップ
            if (touches.Length == 1)
            {
                switch (touches[0].phase)
                {
                    //タッチ開始
                    case TouchPhase.Began:
                        //初期場所登録
                        touchStartPos = touches[0].position;
                        //最後の場所登録()
                        touchEndPos = touches[0].position;
                        break;
                    //動いたとき
                    case TouchPhase.Moved:
                        //スワイプ方向ベクトル生成
                        swipeVec = touchStartPos - touches[0].position;
                        return;

                    //タッチ終了
                    case TouchPhase.Ended:
                        //
                        touchEndPos = touches[0].position;
                        swipeVec = touchStartPos - touchEndPos;
                        moveDist = swipeVec.magnitude;

                        break;
                }
            }

            //マルチタップ
            else if (touches.Length >= 2)
            {
                //全部の指の状態を取得
                TouchPhase[] phase = new TouchPhase[touches.Length];

                //全部の指の分のインスタンス生成
                Vector2[] pos = new Vector2[touches.Length];

                //全部の指の場所を取得
                for (int i = 0; i < touches.Length; i++)
                    pos[i] = touches[i].position;

                //距離を計算
                pinchDist = (pos[0] - pos[1]).magnitude;

                //タッチ開始時のみ初期距離保存
                if (phase[0] == TouchPhase.Began)
                    startPinchDist = pinchDist;

                //今の距離と初めの距離の割合
                pinchRatio = pinchDist / startPinchDist;
                if (pinchRatio > 1.0f)
                    PinchOut();
                else
                    PinchIn();
            }

        }

        //縮める
        private void PinchIn()
        {

        }

        //広げる
        private void PinchOut()
        {

        }

    }
}