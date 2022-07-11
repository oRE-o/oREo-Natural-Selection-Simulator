using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static bool isCameraRoll;
    public float turnSpeed = 4.0f; // 마우스 회전 속도
    public float ySpeed = 100.0f;    
    private float moveSpeed; // 이동 속도 (계속 변할 예정)
    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )
    
    public float speed_fast = 10000.0f;
    public float speed_slow = 80.0f;
    private float d_moveSpeed;
    void Start(){
        transform.eulerAngles = new Vector3(70, 0, 0);
        transform.position = new Vector3(0, 50, -20);
        d_moveSpeed = (speed_fast - speed_slow) * 2;       
    }
    void Update ()
    {
        if (isCameraRoll){
            MouseRotation();
            KeyboardMove();
            yMove();
        }
       
    }
    
    // 마우스의 움직임에 따라 카메라를 회전 시킨다.
    void MouseRotation()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        // Clamp 는 값의 범위를 제한하는 함수
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -90, 90);
    
        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }
    
    // 키보드의 눌림에 따라 이동
    void KeyboardMove()
    {
        if (Input.GetKey("p"))
        {
            moveSpeed = Mathf.Min(moveSpeed + Time.deltaTime * d_moveSpeed, speed_fast);
        }
        else
        {
            moveSpeed = Mathf.Max(moveSpeed - Time.deltaTime * d_moveSpeed, speed_slow);
        }

        var angle_y = transform.rotation.eulerAngles.y;

        var vertical_move = new Vector3(Mathf.Sin(angle_y * Mathf.Deg2Rad), 0f, Mathf.Cos(angle_y * Mathf.Deg2Rad));
        vertical_move *= Input.GetAxis("Vertical");

        angle_y += 90f;
        var horizontal_move = new Vector3(Mathf.Sin(angle_y * Mathf.Deg2Rad), 0f, Mathf.Cos(angle_y * Mathf.Deg2Rad));
        horizontal_move *= Input.GetAxis("Horizontal");

        var move = vertical_move + horizontal_move;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
    void yMove()
    {
        // WASD 키 또는 화살표키의 이동량을 측정
        Vector3 dir = new Vector3(
            0,
            Input.GetAxis("UpDown"),
            0
        );
        // 이동방향 * 속도 * 프레임단위 시간을 곱해서 카메라의 트랜스폼을 이동
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}