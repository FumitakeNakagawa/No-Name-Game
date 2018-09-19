using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomUI
{
    public class RawImage : Base
    {
        [SerializeField]
        public Texture texture;

        //テクスチャがないときの幅・高さ
        [SerializeField]
        int width, height;


        protected bool isRendered;

        protected override void Awake()
        {
            base.Awake();

            //テクスチャデータを基にスプライト作成(テクスチャが無ければ256*256で作成)

            if (texture != null)
                customSprite.CreateSprite(texture.width, texture.height);
            else
                customSprite.CreateSprite(width, height);

            //幅・高さの計算
            rectTransform.sizeDelta = customSprite.textureSize;

            //キャンバスの描画イベントのデリゲートに追加
            Canvas.willRenderCanvases += Render;

            isRendered = false;

        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
            base.Update();
        }


        public void Render()
        {
            if (isRendered)
                return;

            //キャンバスのレンダラーにメッシュ・テクスチャ・マテリアル・カラーデータ格納
            renderer.SetMesh(customSprite.mesh);

            //レンダラーに設定するマテリアル数とマテリアル
            renderer.materialCount = 1;
            if (material != null)
                renderer.SetMaterial(material, 0);
            else
                renderer.SetPopMaterial(Canvas.GetDefaultCanvasMaterial(), 0);


            //テクスチャをセット
            renderer.SetTexture(texture);

            //カラーセット
            renderer.SetColor(color);

           // isRendered = true;
        }


        private void OnApplicationFocus(bool focus)
        {
            isRendered = false;
        }

        protected void OnValidate()
        {
            base.OnValidate();

            if (customSprite == null)
            {
                if (texture != null)
                    customSprite.CreateSprite(texture.width, texture.height);
                else
                    customSprite.CreateSprite(width, height);
            }

            //幅・高さの計算
            rectTransform.sizeDelta = customSprite.textureSize;

        }

        public override void OnCreate()
        {
            base.OnCreate();

        }

        protected override void OnDestroy()
        {
            Canvas.willRenderCanvases -= Render;
            isRendered = false;
            renderer = null;
        }
    }
}
