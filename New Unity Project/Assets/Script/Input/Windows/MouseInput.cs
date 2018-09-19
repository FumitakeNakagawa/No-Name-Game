using UnityEngine;

namespace Windows
{
    class MouseInput : AbstractInput
    {
        public enum MOUSE_BUTTON_NAMECODE
        {
            LEFT_BUTTON,
            RIGHT_BUTTON,
            CENTER_BUTTON,
        }
        public Vector3 mousePosition { get; private set; }
        public Vector3 moveVector;
        public Vector3 total { get; private set; }
        float moveDist;

        public bool IsCursorVisible { get; set; }
        public CursorLockMode cursorLockMode { get; set; }

        public bool isLeftClicked { get; private set; }
        public bool triggerLeftClicked { get; private set; }

        public bool isRightClicked { get; private set; }
        public bool triggerRightClicked { get; private set; }

        public bool isCenterClicked { get; private set; }
        public bool triggerCenterClicked { get; private set; }

        public float mouseWheel { get; private set; }

        public Ray ray;

        public override void Start()
        {
            Cursor.visible = IsCursorVisible;
            Cursor.lockState = cursorLockMode;

            mousePosition = Input.mousePosition;
            moveDist = 0;
            moveVector = Vector3.zero;

            total = Vector3.zero;
        }

        public override void Update()
        {
            //前フレームの位置を保存
            var beforePos = mousePosition;

            //現フレームの位置を取得
            mousePosition = Input.mousePosition;

            //現フレームから前フレーム引いて移動ベクトル生成
            moveVector = mousePosition - beforePos;

            //長さ取得
            moveDist = moveVector.magnitude;

            //正規化ベクトル取得
            moveVector = moveVector.normalized;

            //マウスの総移動量
            var tmp = total + new Vector3(-moveVector.y, moveVector.x, 0) * moveDist;

            //縦方向の向き制限
            tmp.x = Mathf.Clamp(tmp.x, -90f, 90f);

            //代入
            total = tmp;

            bool clicked;
            bool trigger;

            //左クリック
            clicked = isLeftClicked;
            trigger = triggerLeftClicked;
            GetMouseButton(MOUSE_BUTTON_NAMECODE.LEFT_BUTTON, ref clicked, ref trigger);
            isLeftClicked = clicked;
            triggerLeftClicked = trigger;


            //右クリック
            clicked = isRightClicked;
            trigger = triggerRightClicked;
            GetMouseButton(MOUSE_BUTTON_NAMECODE.RIGHT_BUTTON, ref clicked, ref trigger);
            isRightClicked = clicked;
            triggerRightClicked = trigger;


            //中クリック
            clicked = isCenterClicked;
            trigger = triggerCenterClicked;
            GetMouseButton(MOUSE_BUTTON_NAMECODE.CENTER_BUTTON, ref clicked, ref trigger);
            isCenterClicked = clicked;
            triggerCenterClicked = trigger;


            mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            //Debug.Log(mouseWheel.ToString());



        }


        private void Raycasting()
        {
            ray = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit hit = new RaycastHit();

            // Physics.Raycast(ray,);
        }

        public bool Raycasting2D()
        {
            ray = Camera.main.ScreenPointToRay(mousePosition);

            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                return false;
            }

            return true;
        }

        private void GetMouseButton(MOUSE_BUTTON_NAMECODE code, ref bool click, ref bool trigger)
        {
            var flg = Input.GetMouseButton((int)code);
            if (flg && !trigger)
            {
                if (!trigger)
                    trigger = true;

                click = true;
            }
            else
            {
                click = false;
                trigger = false;
            }
        }
    }
}
