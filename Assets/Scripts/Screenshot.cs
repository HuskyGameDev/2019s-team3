using UnityEngine;
using UnityEditor;

/* *
 * Credit to Christian LaCourt for creating this script.
 * */

#if UNITY_EDITOR
public class Screenshot : ScriptableObject
{
    [MenuItem("Tools/Screenshot _F12")]
    private static void Capture()
    {
        var directory = Application.dataPath + "/../Screenshots";
        System.IO.Directory.CreateDirectory(directory);
        var path = string.Format("{0}/Screenshot{1}.png", directory, System.DateTime.Now.Ticks);
        Debug.Log(string.Format("Capture to {0}", path));
        ScreenCapture.CaptureScreenshot(path);
    }
}
#endif