using System.Collections;
using UnityEngine;
using System;

public class WebCameraScript : AbstractInput
{
    //現在利用しているカメラ
    private WebCamDevice nowUsingCamera;

    //全カメラ
    private WebCamDevice[] devices;

    //
    private WebCamTexture camTexture;

    //カメラから取得したテクスチャデータ
    public Texture texture { get; private set; }

    //利用中のカメラ番号
    private int camIndex;

    //
    private Color[] convertColor;


    // Use this for initialization
    public override void Start()
    {
        //利用許可リクエスト
        Application.RequestUserAuthorization(UserAuthorization.WebCam);

        //利用許可がないときはそのまま返す
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            return;

        //全カメラ取得
        devices = WebCamTexture.devices;

        //利用するカメラ取得
        nowUsingCamera = devices[camIndex];

        //カメラインスタンス生成
        camTexture = new WebCamTexture(nowUsingCamera.name, Screen.width, Screen.height, 60);
        camTexture.Play();

        //
        //convertColor = new Color[camTexture.width * camTexture.height];
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            texture = camTexture;
    }


    //左右と上下をそれぞれ逆に向ける関数
    [Obsolete("重い処理でFPSが保てない", false)]
    private IEnumerator InverseTexture()
    {
        while (true)
        {
            Texture2D tex = new Texture2D(camTexture.width, camTexture.height);

            for (int h = 0; h < camTexture.height; h++)
            {
                for (int w = 0; w < camTexture.width; w++)
                {
                    convertColor[camTexture.width * h + w] = camTexture.GetPixel(camTexture.width - w, camTexture.height - h);

                }
            }
            tex.SetPixels(convertColor);
            tex.Apply();


            yield break;
        }
    }


    //アプリがバックグラウンドに移行・復帰時に呼ばれる関数
    public void OnApplicationPause(bool pause)
    {
        //バックグラウンドに移行
        if (pause)
        {
            camTexture.Stop();
            camTexture = null;
        }
        //復帰
        else
        {
            camTexture = new WebCamTexture(nowUsingCamera.name);
            camTexture.Play();

        }
    }


    public void ChangeCamera()
    {
        //利用できるカメラが一種類以下の時は変更しない
        if (devices.Length <= 1)
            return;

        //現在利用時のカメラを停止
        camTexture.Stop();
        camTexture = null;

        //次のカメラデバイスを開く
        camIndex++;
        if (camIndex >= devices.Length)
            camIndex = 0;

        //カメラ取得
        nowUsingCamera = devices[camIndex];
        //カメラインスタンス生成
        camTexture = new WebCamTexture(nowUsingCamera.name);
        //利用開始
        camTexture.Play();
    }
}
