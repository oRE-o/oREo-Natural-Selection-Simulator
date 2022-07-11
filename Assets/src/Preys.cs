using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preys : MonoBehaviour
{
    // Start is called before the first frame 
    public GameObject PreysPrefab;
    public GameObject Predator;

    public static int preycount;
    // == 형질 ==
    public float moveSpeed; // 이속
    public Vector3 currentSize; // 사이즈
    public float turnTime = 3.0f; // 방향 변화하는 시간
    // =========
    public float splitTime = 8.0f;
    // 백엔드 변수
    float currentTime;
    public Vector3 direction; // 바라보는 방향
    float timerTurn; // 방향 변화 타이머
    float timerSplit; // 자식 타이머
    
    void Start()
    {   
        direction = new Vector3(Random.value-0.5f, 0, Random.value-0.5f);
        transform.forward = direction.normalized; // 방향만 남김
        timerTurn = timerSplit = Time.time;
        currentSize = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - timerTurn > turnTime){
            timerTurn = Time.time;
            randomTurn();
        }
        if (Time.time - timerSplit > splitTime){
            timerSplit = Time.time;
            if (preycount <= 100){
                Split();
            }
        }
        // if (Time.time - currentTime > 6){
        //     randomTurn();
        //     currentTime = Time.time;
        //     Split();
        // }
        transform.position += moveSpeed * transform.forward * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -96.6f, 96.6f), transform.position.y, Mathf.Clamp(transform.position.z, -96.6f, 96.6f));
    }

    private void randomTurn(){
        direction = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f);
        transform.forward = direction.normalized;
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Wall"){
            direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
            direction.y = 0;
            // transform.Rotate(transform.up, 90);
            transform.forward = direction.normalized;
        }
        if (collision.gameObject.tag == "Predator"){
            preycount -= 1;
        }
    }

    private void Split(){
        float randomVal = 1 + (Random.value-0.5f)/1.5f; // 0.75 ~ 1.25 사이 값.
        Vector3 newSize = currentSize * randomVal;
        if (newSize.x < 0.25f){
            newSize = new Vector3(0.25f, 0.25f, 0.25f);
        }

        randomVal = 1 + (Random.value-0.5f); // -0.5, 0.5
        float newSpeed = moveSpeed * randomVal;

        randomVal = 1 + (Random.value-0.5f)*1.5f; 
        float newTurnTime = turnTime * randomVal;

        Vector3 newPosition = transform.position;
        newPosition.y = 0.1f + (4 * newSize.y);

        GameObject child = Instantiate(PreysPrefab, newPosition, Quaternion.identity);
        child.transform.localScale = newSize;
        child.GetComponent<Preys>().moveSpeed = newSpeed;
        child.GetComponent<Preys>().turnTime = newTurnTime;
        preycount += 1;
        Debug.Log($"{preycount}");        
    }
}
