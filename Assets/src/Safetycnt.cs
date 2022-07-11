using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Safetycnt : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(delegate {
            if ( GetComponent<Slider>().value.ToString() == "1" )
                a.GetComponent<Text>().text = "On";
            else
                a.GetComponent<Text>().text = "Off";
           
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
