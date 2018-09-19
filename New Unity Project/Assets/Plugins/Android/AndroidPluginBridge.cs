using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPluginBridge : MonoBehaviour
{

    AndroidJavaClass systemAccesserClass;
    string systemAccesserString = "systemaccesser.SystemAccesser";

    AndroidJavaClass unityPlayerClass;
    string unityPlayerString = "com.unity3d.player.UnityPlayer";

    AndroidJavaObject contextObject;


    int brightness = -2;
    // Use this for initialization
    void Start()
    {
        systemAccesserClass = new AndroidJavaClass(systemAccesserString);


        unityPlayerClass = new AndroidJavaClass(unityPlayerString);
        contextObject = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

        brightness = systemAccesserClass.CallStatic<int>("GetBrightness", contextObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (systemAccesserClass != null)
            brightness = systemAccesserClass.CallStatic<int>("GetBrightness", contextObject);

    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle()
        {
            fontSize = 60
        };

        GUILayout.BeginVertical();

        //
        if (unityPlayerClass != null)
            GUILayout.Label("UnityPlayer:" + unityPlayerClass.ToString(), style);
        else
            GUILayout.Label("UnityPlayer:null", style);

        //
        if (contextObject != null)
            GUILayout.Label("context:" + contextObject.ToString(), style);
        else
            GUILayout.Label("context:null", style);

        //
        systemAccesserString = GUILayout.TextField(systemAccesserString, style);

        if (systemAccesserClass != null)
            GUILayout.Label("SystemAccesser:" + systemAccesserClass.ToString(), style);
        else
            GUILayout.Label("SystemAccesser:null", style);

        //
        GUILayout.Label("システムの輝度:" + brightness.ToString(), style);

        if (GUILayout.Button("再検索する", style))
        {
            systemAccesserClass = new AndroidJavaClass(systemAccesserString);


            unityPlayerClass = new AndroidJavaClass(unityPlayerString);
            contextObject = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

            brightness = systemAccesserClass.CallStatic<int>("GetBrightness", contextObject);

        }
        GUILayout.EndVertical();


    }
}
