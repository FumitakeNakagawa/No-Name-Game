using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUI
{
    [ExecuteInEditMode]
    public class Text : Base
    {

        [SerializeField,Multiline]
        string text;

        // Use this for initialization
         protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        void Render()
        {
           
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}