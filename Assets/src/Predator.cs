using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    // 필요한 변인 :
    // 속도, 방향 전환(보는 방향)

    // 벽에 닿으면 튕겨야함, 랜덤한 시간 내에 방향을 변경해야함 
    public float moveSpeed;
    public GameObject Preys;
    public float startTime;
    public Vector3 direction;
    public float turnTime;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.value-0.5f, 0, Random.value-0.5f);
        transform.forward = direction.normalized; // 방향만 남김
        currentTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - currentTime > 2){
            randomTurn();
            currentTime = Time.time;
        }
        transform.position += moveSpeed * transform.forward * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -96.6f, 96.6f), transform.position.y, Mathf.Clamp(transform.position.z, -96.6f, 96.6f));
    }

    private void randomTurn(){
        direction = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f);
        transform.forward = direction.normalized;
    }

    private void OnCollisionEnter(Collision collision){

        if(collision != null){
            if (collision.gameObject.tag == "Prey"){
                Destroy(collision.gameObject);
                return;
            }
            direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
            direction.y = 0;
            // transform.Rotate(transform.up, 90);
            transform.forward = direction.normalized;
            
        }
    }
}
