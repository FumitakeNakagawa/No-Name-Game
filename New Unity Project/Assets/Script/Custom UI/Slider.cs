using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CustomUI
{
    internal class EventOnValueChaged : MonoBehaviour
    {
        private void Start()
        {

        }

        private void Update()
        {

        }

        public void ValueChange()
        {

        }
    }

    public class Slider : Button
    {
        [SerializeField]
        UnityEvent onValueChanged;
        EventOnValueChaged evtOnValueChanged;


        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            onValueChanged.AddListener(delegate { evtOnValueChanged.ValueChange(); });
        }



        // Update is called once per frame
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
        }


    }
}