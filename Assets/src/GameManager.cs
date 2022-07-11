using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject safetyzone;
    public GameObject CoverImage;
    public GameObject MenuImage;
    public GameObject OriginUI;
    public GameObject PreysPrefab;
    public GameObject Predator;
    public GameObject PredatorPrefab;
    // Start is called before the first frame update
    public GameObject PreySlider;
    public GameObject PredatorSlider;
    public GameObject SafetySlider;

    public GameObject MainCamera;
    GameObject[] removedtemp;
    void Start()
    {
        
    }
    public void Initial(float cntPreys, float cntPredator, float cntSafety){
        CameraMove.isCameraRoll = true;
        Time.timeScale = 1.0f;
        MenuImage.SetActive(false);
        for(int i=0; i<cntPreys; i++){
            float randomX = (Random.value-0.5f)*190; // [-95, 95] 에서의 랜덤값
            float randomZ = (Random.value-0.5f)*190;
            Vector3 randomPos = new Vector3(randomX, 4.1f, randomZ);
            GameObject child = Instantiate(PreysPrefab, randomPos, Quaternion.identity);
            child.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            child.GetComponent<Preys>().moveSpeed = 40.0f;
            child.GetComponent<Preys>().turnTime = 3.0f;
            child.GetComponent<Preys>().splitTime = 6.0f;
            if (i == 0){
                Preys.preycount = 1;
            }
            else{
                Preys.preycount += 1;
            }
        
        }
        for(int i=0; i<cntPredator; i++){
            float randomX = (Random.value-0.5f)*190; // [-95, 95] 에서의 랜덤값
            float randomZ = (Random.value-0.5f)*190;
            Vector3 randomPos = new Vector3(randomX, 4.1f, randomZ);
            GameObject child = Instantiate(PredatorPrefab, randomPos, Quaternion.identity);
            child.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            child.GetComponent<Predator>().moveSpeed = 40.0f;
        }

        MainCamera.transform.position = new Vector3(0.7f, 159.1f, -81.5f);
        MainCamera.transform.eulerAngles = new Vector3(70, 0, 0);

        if (cntSafety == 1.0f){
            safetyzone.SetActive(true);
        }
        else{
            safetyzone.SetActive(false);
        }

    }
    public void OnClickPlayButton(){
        CoverImage.SetActive(false);
        Debug.Log($"{PreySlider.GetComponent<Slider>().value}");
        Initial(PreySlider.GetComponent<Slider>().value, PredatorSlider.GetComponent<Slider>().value, SafetySlider.GetComponent<Slider>().value);
        Cursor.visible = false;

    }
    
    public void OnClickExit(){
        Application.Quit();
        
    }
    public void Faster(){
        if(CoverImage.activeInHierarchy){
            Debug.Log("Cannot be Faster");
            return;
        }
        if(MenuImage.activeInHierarchy){
            Debug.Log("Cannot be Faster");
            return;
        }
        if(Time.timeScale == 1.0f){
            Debug.Log("Faster!");
            Time.timeScale = 5f;
            return;
        }
        if(Time.timeScale == 5.0f){
            Debug.Log("Slower!");
            Time.timeScale = 1f;
            return;
        }
        else{
            return;
        }
    }
    public void Pause(){
        if(CoverImage.activeInHierarchy){
            Debug.Log("Cannot be Pause.");
            return;
        }
        if(MenuImage.activeInHierarchy){
            return;
        }    
        if(Time.timeScale == 0f){
            Debug.Log("Play!");
            Time.timeScale = 1f;
        }
        else{
            Debug.Log("Pause!");
            Time.timeScale = 0f;
        }
    }
    public void EscapeMenu(){
        if (CoverImage.activeInHierarchy){
            Debug.Log("cant open menu");
            return;
        }
        else if (MenuImage.activeInHierarchy){
            CameraMove.isCameraRoll = true;
            MenuImage.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Debug.Log("Menu off");
        }
        else {
            CameraMove.isCameraRoll = false;
            MenuImage.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Debug.Log("Menu on");
        }
    }
    public void CamReset(){
        CameraMove.isCameraRoll = false;
        MainCamera.transform.position = new Vector3(0.7f, 159.1f, -81.5f);
        MainCamera.transform.eulerAngles = new Vector3(70, 0, 0);
        MenuImage.SetActive(false);
        Time.timeScale = 1.0f;
        CameraMove.isCameraRoll = true;
    }

    public void BackToMain(){
        CameraMove.isCameraRoll = false;
        MenuImage.SetActive(false);
        CoverImage.SetActive(true);

        removedtemp = GameObject.FindGameObjectsWithTag("Prey");
        for (int i = 0; i<removedtemp.Length; i++){
            Destroy(removedtemp[i]);
        }

        removedtemp = GameObject.FindGameObjectsWithTag("Predator");
        for (int i = 0; i<removedtemp.Length; i++){
            Destroy(removedtemp[i]);
        }
        Preys.preycount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            Faster();
        }
        if (Input.GetKeyDown(KeyCode.F)){
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            CamReset();
            
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            EscapeMenu();
        }
    }
}
