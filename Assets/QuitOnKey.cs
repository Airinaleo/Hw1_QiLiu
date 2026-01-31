using UnityEngine;
using UnityEngine.InputSystem;

public class QuitOnKey : MonoBehaviour
{
    public InputActionReference quitAction;

    private void OnEnable()
    {
        if (quitAction != null)
        {
            quitAction.action.Enable();
            quitAction.action.performed += OnQuitPerformed;
        }
    }

    private void OnDisable()
    {
        if (quitAction != null)
        {
            quitAction.action.performed -= OnQuitPerformed;
        }
    }

    private void OnQuitPerformed(InputAction.CallbackContext context)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("finish");

            AndroidJavaClass processClass = new AndroidJavaClass("android.os.Process");
            int pid = processClass.CallStatic<int>("myPid");
            processClass.CallStatic("killProcess", pid);
        #else
            Application.Quit();
        #endif
    }
}