using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerViewSwitcher : MonoBehaviour
{
    [Header("Teleport Anchors")]
    // 在场景里创建两个空物体，摆放在你想去的地方，然后拖到这里
    public Transform roomAnchor; 
    public Transform externalAnchor; 

    [Header("Input Settings")]
    public InputActionReference toggleViewAction;

    // 默认在房间内
    private bool isInRoom = true;

    private void Start()
    {
        // 初始状态：确保玩家对齐到房间锚点
        if (roomAnchor != null)
        {
            MatchTransform(roomAnchor);
        }
    }

    private void OnEnable()
    {
        if (toggleViewAction != null)
        {
            toggleViewAction.action.Enable();
            toggleViewAction.action.performed += OnToggleView;
        }
    }

    private void OnDisable()
    {
        if (toggleViewAction != null)
        {
            toggleViewAction.action.performed -= OnToggleView;
        }
    }

    private void OnToggleView(InputAction.CallbackContext context)
    {
        // 第一次按下：isInRoom 变成 false，执行去往室外的逻辑
        isInRoom = !isInRoom;

        if (isInRoom)
        {
            MatchTransform(roomAnchor);
        }
        else
        {
            MatchTransform(externalAnchor);
        }
    }

    private void MatchTransform(Transform target)
    {
        if (target == null) return;

        // 设置位置
        transform.position = target.position;

        // 根据要求：控制 Y 轴旋转确保正对目标
        // 我们提取目标锚点的 Y 轴角度，其余轴（X, Z）保持 0 确保玩家是直立的
        float targetYRotation = target.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, targetYRotation, 0);

        Debug.Log($"Player moved to: {target.name}");
    }
}