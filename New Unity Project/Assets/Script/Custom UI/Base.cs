using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CustomUI
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasRenderer))]
    public class Base : Graphic
    {
        //画像の中心軸
        [SerializeField]
        public CanvasManager.PIVOT pivot = CanvasManager.PIVOT.CENTER;

        //スクリーン座標
        [SerializeField]
        public Vector2 position;

        //
        [SerializeField]
        public Vector3 scaleRate = new Vector3(1, 1, 1);

        //マネージャー
        [System.NonSerialized]
        public CanvasManager manager;

        //
        [System.NonSerialized]
        public new RectTransform rectTransform;

        //レンダラー
        [System.NonSerialized]
        public new CanvasRenderer renderer;

        //
        [System.NonSerialized]
        public Sprite customSprite;


        protected override void Awake()
        {
            OnCreate();

        }
        //
        protected override void Start()
        {

        }

        protected virtual void Update()
        {
            if (manager == null) return;


            var screenPos = position;
            //移動
            var pos = manager.ScreenToCanvasSpace2D
                (screenPos, customSprite.textureSize, pivot);
            pos *= manager.ratio;
            rectTransform.anchoredPosition = pos;

            //拡大・縮小
            manager.Scaling(
                ref rectTransform, ref screenPos,
                rectTransform.sizeDelta, customSprite.textureSize,
                manager.ratio, scaleRate,
                pivot);
        }


        protected  void OnValidate()
        {
            //RectTransform取得
            rectTransform = GetComponent<RectTransform>();

            //レンダラー取得
            renderer = GetComponent<CanvasRenderer>();


            //ルートのマネージャー取得
            manager = transform.root.gameObject.GetComponent<CanvasManager>();

            //UIのデフォルトマテリアル取得
            //if (material == null)
            material = Canvas.GetDefaultCanvasMaterial();

            //スプライトインスタンス生成
            customSprite = new Sprite();


            Update();
        }


        public virtual void OnCreate()
        {
            GameObject parent = gameObject;
            while (parent != null && manager == null)
            {
                parent = parent.transform.parent.gameObject;
                manager = parent.GetComponent<CanvasManager>();
            }

            //RectTransform取得
            rectTransform = GetComponent<RectTransform>();

            //レンダラー取得
            renderer = GetComponent<CanvasRenderer>();


            //ルートのマネージャー取得
            manager = transform.root.gameObject.GetComponent<CanvasManager>();

            //UIのデフォルトマテリアル取得
            //if (material == null)
            material = Canvas.GetDefaultCanvasMaterial();

            //スプライトインスタンス生成
            customSprite = new Sprite();

            //アンカーの場所を左下の角に設定
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
        }
    }
}