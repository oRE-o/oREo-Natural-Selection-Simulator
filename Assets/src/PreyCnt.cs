using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreyCnt : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(delegate {
            a.GetComponent<Text>().text = GetComponent<Slider>().value.ToString();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
