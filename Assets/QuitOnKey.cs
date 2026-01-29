using UnityEngine;
using UnityEngine.InputSystem;

public class QuitOnKey : MonoBehaviour
{
    // 绑定Quest 3控制器的退出按键
    public InputAction quitAction;

    private void OnEnable()
    {
        quitAction.Enable();
        quitAction.performed += OnQuitPerformed;
    }

    private void OnDisable()
    {
        quitAction.Disable();
        quitAction.performed -= OnQuitPerformed;
    }

    private void OnQuitPerformed(InputAction.CallbackContext context)
    {
        #if UNITY_EDITOR
            // 编辑器环境：停止播放
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_ANDROID
            // 适配Quest 3（Android）：调用原生API强制退出
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            
            // 1. 关闭当前Activity
            currentActivity.Call("finish");
            
            // 2. 杀死进程（确保完全退出，避免后台残留）
            AndroidJavaClass processClass = new AndroidJavaClass("android.os.Process");
            int pid = processClass.CallStatic<int>("myPid");
            processClass.CallStatic("killProcess", pid);
        #else
            // 其他平台（如PC）：常规退出
            Application.Quit();
        #endif
    }
}