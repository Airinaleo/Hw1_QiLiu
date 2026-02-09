using UnityEngine;

public class LensCameraSync : MonoBehaviour
{
    // 手动指定VR主相机（避免Camera.main失效）
    public Transform vrMainCamera; 
    [Tooltip("放大镜的放大倍数，越小放大越明显")]
    public float zoomFOV = 15f; 

    void Start()
    {
        // 兜底：如果没手动指定，自动找XR主相机（兼容两种方式）
        if (vrMainCamera == null)
        {
            // 优先找XR Origin下的Main Camera（VR专用）
            GameObject xrMainCam = GameObject.Find("Main Camera");
            if (xrMainCam != null)
            {
                vrMainCamera = xrMainCam.transform;
            }
            else
            {
                // 兼容普通场景的Camera.main
                vrMainCamera = Camera.main?.transform;
            }
        }

        // 初始化LensCamera的FOV，保证放大效果
        Camera lensCam = GetComponent<Camera>();
        if (lensCam != null)
        {
            lensCam.fieldOfView = zoomFOV;
        }
    }

    // LateUpdate比Update晚执行，避免和主相机旋转不同步
    void LateUpdate()
    {
        if (vrMainCamera != null)
        {
            // 核心：旋转跟随主相机（玩家视角），位置仍在镜片中心
            transform.rotation = vrMainCamera.rotation;
            // 兜底：强制朝向主相机前方，避免旋转偏移
            transform.LookAt(vrMainCamera.position + vrMainCamera.forward * 100f);
        }
    }
}