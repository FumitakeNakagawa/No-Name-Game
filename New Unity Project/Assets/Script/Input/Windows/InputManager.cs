using UnityEngine;

namespace Windows
{
    class InputManager
    {

        public KeyInput keyInput { get; private set; }
        public MouseInput mouseInput { get; private set; }

        public void Start()
        {
            keyInput = new KeyInput();
            mouseInput = new MouseInput();

            keyInput.Start();
            mouseInput.Start();
        }

        public void Update()
        {
            keyInput.Update();
            mouseInput.Update();
        }
    }
}

