using UnityEngine;
using UnityEngine.InputSystem;

public class FinalViewSwitcher : MonoBehaviour
{
    public Transform roomAnchor;     
    public Transform externalAnchor; 
    public InputActionReference toggleViewAction; 

    private CharacterController cc;
    private bool isInRoom = true;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        // 初始位置：确保第一帧你在房间里且脚踩在地板上
        if (roomAnchor != null) Teleport(roomAnchor);
    }

    void OnEnable()
    {
        if (toggleViewAction != null)
        {
            toggleViewAction.action.Enable();
            toggleViewAction.action.performed += ctx => {
                isInRoom = !isInRoom;
                Teleport(isInRoom ? roomAnchor : externalAnchor);
            };
        }
    }

    void Teleport(Transform target)
    {
        if (target == null || cc == null) return;

        // 核心：关闭物理，防止传送时的碰撞计算导致“掉地洞”
        cc.enabled = false; 

        // 保持高度在地板逻辑点（你可以根据需要微调这里的0.1f）
        Vector3 newPos = new Vector3(target.position.x, target.position.y + 0.05f, target.position.z);
        transform.position = newPos;
        transform.rotation = Quaternion.Euler(0, target.eulerAngles.y, 0);

        cc.enabled = true; // 重新唤醒物理
    }
}