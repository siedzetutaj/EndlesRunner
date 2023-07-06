using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeButtons : MonoBehaviour
{
    public int ID;
    private Player player;
    private GameLogic gameLogic;
    private UpgradesDatabase upgradeDatabase;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        upgradeDatabase = Resources.Load("Upgrades/UpgradeDatabase1") as UpgradesDatabase;
        //trzeba zrobic reference do on clicka i bydzie dzio³oæ
        //this.gameObject.GetComponent<Button>().onClick = this.gameObject.GetComponent<UpgradeButtons>().ShopButton();
    }
    
    private void Update()
    {
        this.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = upgradeDatabase.upgradeStat[ID].name;
    }
    public void ShopButton()
    {
        switch (ID)
        {
            //DoubleJump
            case 0:
                {
                    break;
                }
            //JumpVelocity/strength
            case 1:
                {
                    player.UpgradeHigherJump = UpgradeCost(upgradeDatabase.upgradeStat[1].cost);
                    break;
                }
            case 2:
                {
                    player.UpgradeMaxhealth = UpgradeCost(upgradeDatabase.upgradeStat[2].cost);
                    break;
                }
            case 3:
                {
                    player.UpgradeTimeSlow = UpgradeCost(upgradeDatabase.upgradeStat[3].cost);
                    break;
                }
            case 4:
                {
                    player.UpgradeGun = UpgradeCost(upgradeDatabase.upgradeStat[4].cost);
                    break;
                }
        }
    }
    // whole upgrade button system 
    //trzeba jeszcze dodac zapisywanie do pliku i wczytywanie zeby sie nie resetowa³o
    //przy nowej grze
    private bool UpgradeCost(int price)
    {
        if (price <= gameLogic.gold)
        {
            gameLogic.gold -= price;
            this.gameObject.GetComponent<Button>().interactable = false;
            return true;
        }
        return false;
    }
}
