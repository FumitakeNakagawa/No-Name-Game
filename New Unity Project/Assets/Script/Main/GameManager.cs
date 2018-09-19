using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSceneSetUp(int index)
    {
        StartCoroutine("ChangeScene",index);

    }

    private IEnumerator ChangeScene(int index)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(index,LoadSceneMode.Single);

    }
}
