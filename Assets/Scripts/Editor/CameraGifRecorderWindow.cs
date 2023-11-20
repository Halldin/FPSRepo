using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraGifRecorderWindow : MonoBehaviour
{
    [MenuItem("CameraRecorder/CameraRecorder")]
    static void OpenCustomEditorWindow()
    {
        CameraGifRecorder.Init();
    }
}
