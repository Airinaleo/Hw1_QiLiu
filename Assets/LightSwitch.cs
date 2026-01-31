using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light lightComponent;
    public InputActionReference switchLightAction;

    public Color color1 = Color.white;
    public Color color2 = new Color(1f, 0.5f, 0f); 
    public float intensity1 = 90f;
    public float intensity2 = 120f;

    private bool isColor1 = true;

    void Start()
    {
        if (lightComponent == null) lightComponent = GetComponent<Light>();
        
        lightComponent.color = color1;
        lightComponent.intensity = intensity1;
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
        isColor1 = !isColor1;
        lightComponent.color = isColor1 ? color1 : color2;
        lightComponent.intensity = isColor1 ? intensity1 : intensity2;
    }
}