using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour
{
    public int gold =0;
    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI distanceDisplay;
    public float distance;
    [SerializeField]
    public GameObject UI;
    void Start()
    {
        //UI = GameObject.Find("UI");
        UI.SetActive(true);
        goldDisplay = GameObject.Find("coins").GetComponent<TextMeshProUGUI>();
        distanceDisplay = GameObject.Find("distance").GetComponent <TextMeshProUGUI>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        goldDisplay.text = gold.ToString();
        distanceDisplay.text = math.round(distance).ToString();
    }
}
