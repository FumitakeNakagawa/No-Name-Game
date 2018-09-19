using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject prefabs;

    [SerializeField, Header("各項目の下限・上限")]
    private Vector3 min, max;

    [SerializeField, Header("ターゲットの最大同時出現数")]
    int maxTarget;


    int targetCount;

    public delegate void DelegateCountDown();
    public static event DelegateCountDown EvtCountDown;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Add");
        targetCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Append()
    {
        var rand = ((int)Random.Range(0, 10));

        //ポジションランダム生成
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(min.x, max.x);
        pos.y = Random.Range(min.y, max.y);
        pos.z = Random.Range(min.z, max.z);
        if ((rand & 0x1) == 1)
        {
            pos.x *= -1;
            pos.z *= -1;
        }

        Vector3 rotation = new Vector3(-90, 0, 0);

        //的を生成
        var mato = Instantiate(prefabs);

        //座標割り当て
        mato.transform.position = pos;

        //プレイヤーに向けてる
        var vec = pos - player.transform.position;

        var dot = Vector3.Dot(vec.normalized, Vector3.forward);
        var deg = Mathf.Acos(dot) * Mathf.Rad2Deg;

        rotation.z = deg;
        var cross = Vector3.Cross(vec.normalized, Vector3.forward);


        mato.transform.rotation = Quaternion.Euler(rotation);
        mato.transform.parent = transform;

    }

    IEnumerator Add()
    {
        //5秒に一回 的 生成
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (targetCount < maxTarget)
                Append();
        }
    }

    //的が消えるときにカウントダウン
    public void CountDown()
    {
        targetCount--;
        if (targetCount < 0)
            targetCount = 0;
    }
}
