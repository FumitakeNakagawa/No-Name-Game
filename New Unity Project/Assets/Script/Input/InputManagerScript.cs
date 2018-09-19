using System;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Threading;

class InputManagerScript : MonoBehaviour
{
    public string filename;

#if UNITY_STANDALONE || UNITY_EDITOR

    //Windows版の入力マネージャー
    public Windows.InputManager inputManager { get; private set; }
    // Use this for initialization

    //カーソル表示の有無
    [SerializeField]
    bool cursorVisible = true;

    //カーソルのモード
    [SerializeField]
    CursorLockMode cursorLockMode = CursorLockMode.None;

#elif UNITY_ANDROID 

    //Android版の入力マネージャー
   public Android.InputManager inputManager{get; private set;}

#endif

    //ウェブカメラ
    public WebCameraScript webCameraScript { get; private set; }

    // Use this for initialization
    void Start()
    {
        filename = null;
#if UNITY_STANDALONE ||UNITY_EDITOR

        //入力マネージャーインスタンス生成・初期化
        inputManager = new Windows.InputManager();
        inputManager.Start();

        inputManager.mouseInput.IsCursorVisible = cursorVisible;
        //inputManager.mouseInput.cursorLockMode = cursorLockMode;

#elif UNITY_ANDROID

        //入力マネージャーインスタンス生成・初期化
        inputManager = new Android.InputManager();
        inputManager.Start();
#endif

        //ウェブカメラインスタンス生成・初期化
        webCameraScript = new WebCameraScript();
        webCameraScript.Start();
    }

    // Update is called once per frame
    void Update()
    {

        inputManager.Update();
        webCameraScript.Update();
    }

    private void OnGUI()
    {
#if UNITY_STANDALONE || UNITY_EDITOR

#elif UNITY_ANDROID

        GUI.Label(new Rect(0, 0, 1000, 400), filename);
#endif
    }


    public void ChangeCamera()
    {
        webCameraScript.ChangeCamera();
    }

    //画面をキャプチャー(スクリーンショット)
    public void CaptureScreen()
    {
        //コルーチン呼び出し
        StartCoroutine("CaptureScreenCoroutine");
    }

    //コルーチンでキャプチャー処理
    IEnumerator CaptureScreenCoroutine()
    {
        yield return new WaitForEndOfFrame();

        //現在のレンダーテクスチャを保存(nullは描画先が画面)
        RenderTexture currenRT = RenderTexture.active;

        //スクリーンサイズ取得
        var width = Screen.width;
        var height = Screen.height;

        //新しいレンダーテクスチャを生成
        RenderTexture rt = new RenderTexture(width, height, 32);

        //描画先変更
        Camera.main.targetTexture = rt;

        //手動で描画
        Camera.main.Render();

        //レンダーテクスチャを変更
        RenderTexture.active = rt;

        //画像生成
        Texture2D tex2D = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex2D.Apply();

        //PNG生成
        var data = tex2D.EncodeToPNG();

        string path = Application.persistentDataPath;

        //ファイル名指定
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();
        string hour = DateTime.Now.Hour.ToString();
        string minute = DateTime.Now.Minute.ToString();
        string second = DateTime.Now.Second.ToString();
        string nowTime = year + "." + month + "." + day + "." + hour + "-" + minute + "-" + second;


#if UNITY_ANDROID
        string rootPath = null;
        string key = null;
        int i = 0;
        //"Android"という名のフォルダまでのパスを取得(閲覧可能な直下に当たる)
        while (i < path.Length)
        {
            if (path[i] == '/')
            {
                if (key == "Android")
                    break;
                else
                {
                    rootPath += key + '/';
                    key = null;
                }
            }
            else
                key += path[i];
            i++;
        }
        path = rootPath;
#endif

        path += "PICTURES";

        //"PICTURES"というフォルダ名がなければ作成
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        //フルパス+ファイル名割り当て
        filename = path + "/" + nowTime + ".png";

        //バイナリデータに変換
        File.WriteAllBytes(filename, data);


        Destroy(tex2D);

        //レンダリング先を元に戻す
        RenderTexture.active = currenRT;

        //カメラのレンダーターゲットを画面に戻す
        Camera.main.targetTexture = null;
    }


}
