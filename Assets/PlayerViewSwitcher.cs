using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleViewSwitcher : MonoBehaviour
{
    public Transform roomAnchor;     
    public Transform externalAnchor; 
    public InputActionReference toggleViewAction; 

    private CharacterController cc;
    private readonly float groundOffset = 0.1f;
    // 新增：标记是否在移动中，避免切换视角打断移动
    private bool isMoving = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if (roomAnchor != null)
        {
            transform.position = new Vector3(roomAnchor.position.x, groundOffset, roomAnchor.position.z);
        }
        // 移除：不再强制修改CC参数，改用ContinuousMove的配置
        // cc.center = new Vector3(0, 0.85f, 0);
        // cc.height = 1.7f;
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

    // 新增：检测是否在移动（避免切换视角打断移动）
    void Update()
    {
        isMoving = cc.velocity.magnitude > 0.1f;
    }

    void SwitchView(InputAction.CallbackContext ctx)
    {
        // 仅当未移动时切换视角，避免打断移动
        if (isMoving) return;

        Transform target = transform.position == new Vector3(roomAnchor.position.x, groundOffset, roomAnchor.position.z) 
            ? externalAnchor : roomAnchor;
        
        // 优化：不禁用CC，改用忽略碰撞的方式切换位置
        Vector3 newPos = new Vector3(target.position.x, groundOffset, target.position.z);
        // 平滑移动，避免强制覆盖位置
        transform.position = Vector3.Lerp(transform.position, newPos, 0.2f);
        transform.rotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
    }
}