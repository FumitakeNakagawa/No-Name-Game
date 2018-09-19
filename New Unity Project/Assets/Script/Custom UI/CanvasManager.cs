using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CustomUI
{
    [ExecuteInEditMode]
    [RequireComponent(
        typeof(Canvas),
        typeof(GraphicRaycaster)
        )]
    public class CanvasManager : MonoBehaviour
    {
        public enum PIVOT
        {
            LEFT_UP,
            UP,
            RIGHT_UP,
            LEFT,
            CENTER,
            RIGHT,
            LEFT_DOWN,
            DOWN,
            RIGHT_DOWN
        }


        [SerializeField]
        public Vector2Int requestScreenSize = new Vector2Int(1920, 1080);
        public Vector2Int screenSize { get; private set; }
        public Vector2 ratio { get; private set; }

        public Canvas canvas { get; private set; }
        public GraphicRaycaster graphicRaycaster { get; private set; }

        public EventSystem eventSystem { get; private set; }

        private void Awake()
        {
            canvas = gameObject.GetComponent<Canvas>();
            graphicRaycaster = gameObject.GetComponent<GraphicRaycaster>();

            screenSize = new Vector2Int(Screen.width, Screen.height);


            ratio = new Vector2(
                (float)screenSize.x / requestScreenSize.x,
                (float)screenSize.y / requestScreenSize.y);


            //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        // Use this for initialization
        void Start()
        {

        }

        private void Update()
        {



        }

        public Vector3 ScreenToCanvasSpace2D
            (Vector2 screenPos, Vector2 texSize, PIVOT pivot)
        {

            var pos = new Vector2(screenPos.x, screenPos.y);

            var width = texSize.x;
            var height = texSize.y;

            switch (pivot)
            {
                case PIVOT.LEFT_UP:
                    pos.x += width / 2;
                    pos.y -= height / 2;
                    break;
                case PIVOT.UP:

                    pos.y -= height / 2;
                    break;
                case PIVOT.RIGHT_UP:
                    pos.x -= width / 2;
                    pos.y -= height / 2;
                    break;
                case PIVOT.LEFT:
                    pos.x += width / 2;
                    break;
                case PIVOT.CENTER:

                    break;
                case PIVOT.RIGHT:
                    pos.x -= width / 2;

                    break;
                case PIVOT.LEFT_DOWN:
                    pos.x += width / 2;
                    pos.y += height / 2;
                    break;
                case PIVOT.DOWN:

                    pos.y += height / 2;
                    break;
                case PIVOT.RIGHT_DOWN:
                    pos.x -= width / 2;
                    pos.y += height / 2;
                    break;
            }

            return pos;
        }


        public Vector3 ScreenToCanvasSpace2D
        (ref RectTransform transform, Vector2 screenPos, Vector2 texSize, PIVOT pivot)
        {

            var pos = new Vector2(screenPos.x, screenPos.y);

            var width = texSize.x;
            var height = texSize.y;

            switch (pivot)
            {
                case PIVOT.LEFT_UP:
                    pos.x += width / 2;
                    pos.y -= height / 2;

                    transform.anchorMin.Set(pos.x, pos.y - height);
                    transform.anchorMax.Set(pos.x + width, pos.y);

                    break;
                case PIVOT.UP:

                    pos.y -= height / 2;
                    break;
                case PIVOT.RIGHT_UP:
                    pos.x -= width / 2;
                    pos.y -= height / 2;
                    break;
                case PIVOT.LEFT:
                    pos.x += width / 2;
                    break;
                case PIVOT.CENTER:

                    break;
                case PIVOT.RIGHT:
                    pos.x -= width / 2;

                    break;
                case PIVOT.LEFT_DOWN:
                    pos.x += width / 2;
                    pos.y += height / 2;
                    break;
                case PIVOT.DOWN:

                    pos.y += height / 2;
                    break;
                case PIVOT.RIGHT_DOWN:
                    pos.x -= width / 2;
                    pos.y += height / 2;
                    break;
            }

            return pos;
        }



        public void Scaling(ref RectTransform rectTransform, ref Vector2 pos, Vector2 baseSize, Vector2 texSize, Vector2 ratio, Vector3 scaleRate, PIVOT pivot)
        {
           // rectTransform.sizeDelta = texSize * scaleRate;
            rectTransform.localScale = ratio * scaleRate;

            /*
            switch (pivot)
            {
                case PIVOT.LEFT_UP:
                    pos.x = pos.x - (baseSize.x - rectTransform.sizeDelta.x);
                    pos.y = pos.y + (baseSize.y - rectTransform.sizeDelta.y);

                    break;
                case PIVOT.UP:

                    pos.y = pos.y + (baseSize.y - rectTransform.sizeDelta.y);
                    break;
                case PIVOT.RIGHT_UP:
                    pos.x = pos.x + (baseSize.x - rectTransform.sizeDelta.x);
                    pos.y = pos.y + (baseSize.y - rectTransform.sizeDelta.y);

                    break;
                case PIVOT.LEFT:
                    pos.x = pos.x - (baseSize.x - rectTransform.sizeDelta.x);

                    break;
                case PIVOT.CENTER:

                    break;
                case PIVOT.RIGHT:
                    pos.x = pos.x + (baseSize.x - rectTransform.sizeDelta.x);

                    break;
                case PIVOT.LEFT_DOWN:
                    pos.x = pos.x - (baseSize.x - rectTransform.sizeDelta.x);
                    pos.y = pos.y - (baseSize.y - rectTransform.sizeDelta.y);
                    break;
                case PIVOT.DOWN:

                    pos.y = pos.y - (baseSize.y - rectTransform.sizeDelta.y);
                    break;
                case PIVOT.RIGHT_DOWN:
                    pos.x = pos.x + (baseSize.x - rectTransform.sizeDelta.x);
                    pos.y = pos.y - (baseSize.y - rectTransform.sizeDelta.y);

                    break;
            }
            */
        }

    }

}