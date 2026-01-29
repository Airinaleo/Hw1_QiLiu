using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    // 灯光组件引用（已在Inspector赋值）
    public Light lightComponent;

    // Quest 3控制器绑定的切换动作
    public InputAction switchLightAction;

    // 可在Inspector调整的颜色和亮度参数
    public Color color1 = Color.white;
    public Color color2 = new Color(1f, 0.5f, 0f); // 明亮橙色
    public float intensity1 = 90f;    // 匹配你Inspector里的初始亮度
    public float intensity2 = 120f;   // 切换后的更高亮度，增强视觉反馈

    private bool isColor1 = true;

    void Start()
    {
        // 自动获取灯光组件（防止Inspector未赋值）
        if (lightComponent == null)
        {
            lightComponent = GetComponent<Light>();
        }
        // 初始化灯光状态
        lightComponent.color = color1;
        lightComponent.intensity = intensity1;
        Debug.Log("灯光初始化完成：颜色=" + color1 + "，亮度=" + intensity1);
    }

    private void OnEnable()
    {
        switchLightAction.Enable();
        switchLightAction.performed += OnSwitchLight;
    }

    private void OnDisable()
    {
        switchLightAction.Disable();
        switchLightAction.performed -= OnSwitchLight;
    }

    // 控制器按键触发的切换逻辑
    private void OnSwitchLight(InputAction.CallbackContext context)
    {
        // 切换状态
        isColor1 = !isColor1;

        // 应用颜色和亮度（高对比度+高亮度，VR里清晰可见）
        lightComponent.color = isColor1 ? color1 : color2;
        lightComponent.intensity = isColor1 ? intensity1 : intensity2;

        // 调试日志（打包后可通过Logcat查看）
        Debug.Log("灯光切换：颜色=" + lightComponent.color + "，亮度=" + lightComponent.intensity);
    }
}