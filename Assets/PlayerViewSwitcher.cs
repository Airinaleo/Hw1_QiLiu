using UnityEngine;
using UnityEngine.InputSystem;

// 仅保留视角切换+Y轴抬高核心逻辑，新手零出错
public class SimpleViewSwitcher : MonoBehaviour
{
    public Transform roomAnchor;     // 拖入根目录的room point
    public Transform externalAnchor; // 拖入根目录的external point
    public InputActionReference toggleViewAction; // 绑定手柄按键

    private CharacterController cc;
    // 手柄/XR Origin离地高度（固定0.1米，避免贴地面）
    private readonly float groundOffset = 0.1f;

    void Start()
    {
        // 抓取CharacterController，设置初始位置
        cc = GetComponent<CharacterController>();
        if (roomAnchor != null)
        {
            // 初始位置：锚点X/Z + 抬高Y轴0.1米
            transform.position = new Vector3(roomAnchor.position.x, groundOffset, roomAnchor.position.z);
        }
        // 强制抬高CharacterController，避免贴地
        cc.center = new Vector3(0, 0.85f, 0);
        cc.height = 1.7f;
    }

    void OnEnable()
    {
        if (toggleViewAction != null)
        {
            toggleViewAction.action.Enable();
            toggleViewAction.action.performed += SwitchView;
        }
    }

    void OnDisable()
    {
        if (toggleViewAction != null)
        {
            toggleViewAction.action.performed -= SwitchView;
            toggleViewAction.action.Disable();
        }
    }

    // 核心：切换视角时始终抬高Y轴
    void SwitchView(InputAction.CallbackContext ctx)
    {
        // 切换锚点
        Transform target = transform.position == new Vector3(roomAnchor.position.x, groundOffset, roomAnchor.position.z) 
            ? externalAnchor : roomAnchor;
        
        // 禁用CC避免物理冲突，设置位置（强制抬高Y轴）
        cc.enabled = false;
        transform.position = new Vector3(target.position.x, groundOffset, target.position.z);
        transform.rotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
        cc.enabled = true;
    }
}