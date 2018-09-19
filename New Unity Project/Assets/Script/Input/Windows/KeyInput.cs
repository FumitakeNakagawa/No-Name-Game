using UnityEngine;

namespace Windows
{

    class KeyInput : AbstractInput
    {
        Vector3 move;
        public Quaternion rotation { get; private set; }

        public override void Start()
        {
            move = Vector3.zero;
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                move += new Vector3(0, -3, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move += new Vector3(0, 3, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                move += new Vector3(-3, 0, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move += new Vector3(3, 0, 0) * Time.deltaTime;
            }

            rotation = Quaternion.Euler(move);
        }
    }
}
