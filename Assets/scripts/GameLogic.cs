using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour
{
    //staff to groundScript
    public static float TimeBetwenShootingResp = 0;
    //UIDisplay
    public int health=3;
    public int gold =0;
    public TextMeshProUGUI[] goldDisplay;
    public TextMeshProUGUI distanceDisplay;
    public TextMeshProUGUI healthDisplay;
    public float GameLogicDistance;
    public GameObject FuelDisplay;
    public float FuelAmount=1;
    public float MaxFuelAmount;

    //Whole UI
    public GameObject UI;
    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public GameObject Shop;
    public GameObject PausePanel;
    //life and death staff
    private bool IsDead = false;
    void Start()
    {
        TimeBetwenShootingResp = 0;
        UI.SetActive(true);
        GamePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        Shop.SetActive(false);
        PausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        foreach (TextMeshProUGUI coins in goldDisplay)
        {
            coins.text = gold.ToString();
        }
        distanceDisplay.text = math.round(GameLogicDistance).ToString();
        healthDisplay.text = health.ToString();
        GameOver(health);
        FuelamountDisplay();
        CoursorVisibility();
        PuseMenu();
        TimeBetwenShootingResp -= Time.deltaTime;
    }
    private void GameOver(int health)
    {
        if (health <= 0 && !IsDead)  
        {
            GameOverPanel.SetActive(true);
            IsDead = true;
            Time.timeScale = 0;
        }
    }
    private void CoursorVisibility()
    {
        if (IsDead)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ButtonReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ButtonShop()
    {
        GameOverPanel.SetActive(false);
        Shop.SetActive(true);
    }
    public void ButtonQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        
    }
    public void ButtonStart()
    {
        SceneManager.LoadScene("Game");
    }
    public void ButtonResume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FuelamountDisplay()
    {
        float fuelDisplayAmount = FuelAmount / MaxFuelAmount;
        FuelDisplay.GetComponent<Slider>().value = fuelDisplayAmount;
    }
    public void PuseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PausePanel.activeSelf&&!IsDead)
            {
                PausePanel.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(PausePanel.activeSelf)
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
