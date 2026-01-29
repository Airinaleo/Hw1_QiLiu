using UnityEngine;
using UnityEngine.InputSystem;

// 挂载到XR Origin（玩家对象）上，控制位置切换
public class PlayerPositionSwitch : MonoBehaviour
{
    [Header("切换位置配置")]
    public Vector3 roomPosition = new Vector3(0, 1.6f, 0);       // 房间初始位置（匹配XR Origin初始位置）
    public Quaternion roomRotation = Quaternion.identity;        // 房间初始旋转（无旋转）
    public Vector3 externalPosition = new Vector3(10, 2, 5);     // 外部观察点位置（匹配ExternalViewPoint）
    public Quaternion externalRotation = Quaternion.identity;    // 外部观察点旋转（和ExternalViewPoint保持一致，Y=0）

    // 绑定Quest 3控制器按键（切换触发）
    public InputAction switchViewAction;

    // 标记当前位置（true=房间内，false=外部）
    private bool isInRoom = true;

    // 游戏启动时初始化：确保玩家从房间内开始
    void Start()
    {
        // 强制初始位置为房间位置，避免场景加载时的偏移
        transform.SetPositionAndRotation(roomPosition, roomRotation);
        Debug.Log("初始位置设置完成：" + roomPosition + "，旋转：" + roomRotation);
    }

    // 启用脚本时激活输入动作
    private void OnEnable()
    {
        switchViewAction.Enable();
        switchViewAction.performed += OnSwitchView;
    }

    // 禁用脚本时关闭输入动作
    private void OnDisable()
    {
        switchViewAction.Disable();
        switchViewAction.performed -= OnSwitchView;
    }

    // 按键触发：交替切换位置
    private void OnSwitchView(InputAction.CallbackContext context)
    {
        // 切换位置标记
        isInRoom = !isInRoom;

        // 根据标记设置位置/旋转
        if (isInRoom)
        {
            transform.SetPositionAndRotation(roomPosition, roomRotation);
            Debug.Log("切换到房间位置：" + roomPosition);
        }
        else
        {
            transform.SetPositionAndRotation(externalPosition, externalRotation);
            Debug.Log("切换到外部观察点：" + externalPosition);
        }
    }
}