using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class WebCameraSpriteScript : MonoBehaviour
{
    //入力マネージャー
    [SerializeField]
    private InputManagerScript inputManagerScript;

    RawImage image;

    [SerializeField]
    bool xInverse, yInverse;

    [SerializeField]
    CustomUI.CanvasManager manager;

    private void Start()
    {

        image = GetComponent<RawImage>();

        //image.customSprite.CreateSprite(Screen.width, Screen.height);
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        var scale = transform.localScale;
        scale.x = xInverse ? -1 : 1;
        scale.y = yInverse ? -1 : 1;
        transform.localScale = scale;
    }

    private void Update()
    {
        image.material.mainTexture = inputManagerScript.webCameraScript.texture;
        image.texture = image.material.mainTexture;
    }
}