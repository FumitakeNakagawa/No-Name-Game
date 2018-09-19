using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CustomUI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public class Button : Selectable
    {
        Image image;

        [SerializeField]
        UnityEvent onClickDown;

        [SerializeField]
        UnityEvent onClickUp;

        bool isPressed;


        protected override void Start()
        {
            base.Start();
            image = GetComponent<Image>();
            targetGraphic = image;
            isPressed = false;
        }

        
        private void Update()
        {
            //ボタンを押し続けている間
            if (isPressed)
                onClickDown.Invoke();
            
        }


        //ボタンを押したときのトリガーイベント
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            image.color = colors.pressedColor;
            isPressed = true;
        }

        //ボタンの領域内に入っている時
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            image.color = colors.highlightedColor;

        }

        //ボタンの領域外に移動
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            image.color = colors.normalColor;
            isPressed = false;
        }

        //ボタンを離したとき
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            image.color = colors.highlightedColor;
            isPressed = false;

            onClickUp.Invoke();
        }
    }
}
