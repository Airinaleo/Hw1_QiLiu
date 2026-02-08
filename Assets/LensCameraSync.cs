using UnityEngine;

public class LensCameraSync : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // 自动找到 VR 里的眼睛（主相机）
        mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            // 关键：位置跟着放大镜走，但旋转永远和眼睛保持一致
            // 这样你斜着拿放大镜，里面的世界也是正的
            transform.rotation = mainCameraTransform.rotation;
        }
    }
}