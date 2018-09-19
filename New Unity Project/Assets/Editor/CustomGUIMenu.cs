using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CustomGUIMenu
{
    //Custom GUI のタブを作成
    [MenuItem("GameObject/Custom GUI", false, 20)]

    //Custom GUIのにCanvasを作成
    [MenuItem("GameObject/Custom GUI/Canvas", false, 0)]
    public static void AddCanvas()
    {
        CreateGUI_Object("Canvas", typeof(CustomUI.CanvasManager));

    }


    //Custom GUIのにRaw Imageを作成
    [MenuItem("GameObject/Custom GUI/Raw Image", false, 0)]
    public static void AddRawImage()
    {
        var canvas = SearchCanvas();
        var rawImage = CreateGUI_Object("Raw Image", typeof(CustomUI.RawImage));

        var selection = Selection.activeGameObject;
        if (selection != null)
            rawImage.transform.SetParent(selection.transform);
        else
            rawImage.transform.SetParent(canvas.transform);

        var componet = rawImage.GetComponent<CustomUI.RawImage>();
        componet.OnCreate();
    }


    [MenuItem("GameObject/Custom GUI/Image", false, 1)]
    public static void AddImage()
    {
        var canvas = SearchCanvas();

        var rawImage = CreateGUI_Object("Image", typeof(CustomUI.Image));

        var selection = Selection.activeGameObject;
        if (selection != null)
            rawImage.transform.SetParent(selection.transform);
        else
            rawImage.transform.SetParent(canvas.transform);
    }


    //Custom GUI の中にボタンを作成
    [MenuItem("GameObject/Custom GUI/Button", false, 2)]
    public static void AddButton()
    {
        var canvas = SearchCanvas();

        var rawImage = CreateGUI_Object("Button", typeof(CustomUI.Button));

        var selection = Selection.activeGameObject;
        if (selection != null)
            rawImage.transform.SetParent(selection.transform);
        else
            rawImage.transform.SetParent(canvas.transform);
    }


    // todo 未完成
    [MenuItem("GameObject/Custom GUI/Text", false, 3)]
    public static void AddText()
    {
        var canvas = SearchCanvas();

        var rawImage = CreateGUI_Object("Text", typeof(CustomUI.RawImage));

        var selection = Selection.activeGameObject;
        if (selection != null)
            rawImage.transform.SetParent(selection.transform);
        else
            rawImage.transform.SetParent(canvas.transform);
    }


    //ゲームオブジェクト作成関数(マクロ関数)
    private static GameObject CreateGUI_Object(string name, System.Type component)
    {
        GameObject gameObject = new GameObject();
        //gameObject.AddComponent<CustomUI.Base>();
        gameObject.name = name;
        gameObject.AddComponent(component);
        return gameObject;
    }


    //キャンバス検索(無ければ作成)
    private static GameObject SearchCanvas()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null)
            CreateGUI_Object("Canvas", typeof(CustomUI.CanvasManager));

        return canvas;
    }

}