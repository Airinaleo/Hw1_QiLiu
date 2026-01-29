using UnityEngine;

// 挂载到行星（Planet）对象上，控制绕Y轴旋转
public class PlanetOrbit : MonoBehaviour
{
    // 旋转速度（单位：度/秒），可在Inspector调整
    public float rotateSpeed = 30f;

    // Update每一帧执行，用Time.deltaTime保证帧率无关
    void Update()
    {
        // 绕Y轴旋转：transform.Rotate(轴, 角度*Time.deltaTime)
        // Vector3.up 等价于 (0,1,0)，即Y轴
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}