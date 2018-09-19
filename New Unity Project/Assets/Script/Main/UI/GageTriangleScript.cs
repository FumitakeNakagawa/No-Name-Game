using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUI;

public class GageTriangleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject triangleUI;

    [SerializeField]
    private PlayerScript playerScript;

    private Vector2 pos, startPos, endPos;

    private Image gageImage;
    private Image triangleImage;

    private float moveRate;
    private float frameMoveRate;

    // Use this for initialization
    void Start()
    {
        gageImage = GetComponent<Image>();

        triangleImage = triangleUI.GetComponent<Image>();

        //ゲージのピント開始位置保存
        startPos = triangleImage.position;
        pos = startPos;

        //ゲージのピント最終位置保存(解像度によって変わるので移動比率計算)
        endPos = startPos + (gageImage.rectTransform.sizeDelta/gageImage.manager.ratio);

        //秒間の移動量
        moveRate = (endPos.x - startPos.x) / playerScript.maxStoreTime;

    }

    // Update is called once per frame
    void Update()
    {

        triangleImage.position = pos;
    }

    public void StoringTheForce()
    {
        //フレーム間の移動量
        frameMoveRate = moveRate * Time.deltaTime;

        //ピントを動かす
        pos.x += frameMoveRate;
        pos.x = Mathf.Min(pos.x, endPos.x);
    }


    public void Release()
    {
        //開始位置へ
        pos = startPos;
    }
}
