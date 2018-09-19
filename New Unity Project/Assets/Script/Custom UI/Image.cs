using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUI
{
    public class Image : Base
    {
        //テクスチャ
        [SerializeField]
        public UnityEngine.Sprite texture;

        //テクスチャがないときの幅・高さ
        [SerializeField]
        int width, height;


        public UnityEngine.Sprite GetTexture() { return texture; }

        protected override void Awake()
        {
            base.Awake();

            //スプライト生成
            if (texture != null)
                customSprite.CreateSprite(texture.texture.width, texture.texture.height);
            else
                customSprite.CreateSprite(width, height);

            //RectTransformに幅・高さ保存
            rectTransform.sizeDelta = customSprite.textureSize;

            //キャンバスの描画イベントに追加
            Canvas.willRenderCanvases += Render;

        }
       
        
        // Use this for initialization
        protected override void Start()
        {
        }


        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }


        public void Render()
        {

            renderer.SetMesh(customSprite.mesh);
            renderer.materialCount = 1;
            renderer.SetMaterial(material, 0);

            if (texture != null)
                renderer.SetTexture(texture.texture);

            renderer.SetColor(color);
        }

        protected new void OnDestroy()
        {
            Canvas.willRenderCanvases -= Render;
        }

        protected  void OnValidate()
        {
            base.OnValidate();

            //スプライト生成
            if (texture != null)
                customSprite.CreateSprite(texture.texture.width, texture.texture.height);
            else
                customSprite.CreateSprite(width, height);

            //RectTransformに幅・高さ保存
            rectTransform.sizeDelta = customSprite.textureSize;

        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}