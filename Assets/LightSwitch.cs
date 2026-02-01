using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light lightComponent;
    public InputActionReference switchLightAction;

    // 预设颜色：#FFF262（黄色）和 #35E5DD（青色）
    public Color color1 = new Color(1f, 0.949f, 0.384f); 
    public Color color2 = new Color(0.208f, 0.898f, 0.867f); 
    public float intensity1 = 2.5f;   // 对应你Inspector里的Intensity 1
    public float intensity2 = 70f;    // 对应你Inspector里的Intensity 2

    // 状态：0=关灯，1=颜色1（黄色），2=颜色2（青色）
    private int currentState = 1;

    void Start()
    {
        if (lightComponent == null) 
            lightComponent = GetComponent<Light>();
        
        // 初始状态为黄色灯光
        UpdateLightState();
    }

    private void OnEnable()
    {
        if (switchLightAction != null)
        {
            switchLightAction.action.Enable();
            switchLightAction.action.performed += OnSwitchLight;
        }
    }

    private void OnDisable()
    {
        if (switchLightAction != null)
        {
            switchLightAction.action.performed -= OnSwitchLight;
        }
    }

    private void OnSwitchLight(InputAction.CallbackContext context)
    {
        // 循环切换状态：0→1→2→0
        currentState = (currentState + 1) % 3;
        UpdateLightState();
    }

    // 根据当前状态更新灯光
    private void UpdateLightState()
    {
        switch (currentState)
        {
            case 0: // 关灯
                lightComponent.enabled = false;
                break;
            case 1: // 黄色灯光
                lightComponent.enabled = true;
                lightComponent.color = color1;
                lightComponent.intensity = intensity1;
                break;
            case 2: // 青色灯光
                lightComponent.enabled = true;
                lightComponent.color = color2;
                lightComponent.intensity = intensity2;
                break;
        }
    }
}