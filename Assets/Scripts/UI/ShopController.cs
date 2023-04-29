using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] Button restoreButton;
    [SerializeField] Button increaseButton;

    [SerializeField] GameObject[] lifeSprites;
    [SerializeField] TextMeshProUGUI moneyDisplay;

    public PlayerInventory inv;
    public PlayerStats stats;

    private void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    //restores player's health - either a set increment or up to the max
    public void RestoreHealth()
    {
        //for now, fully healing based on current max health
        stats.Heal(100);
        UpdateMenu();
    }

    //increases the player's max health
    public void IncreaseMaxHealth()
    {
        UpdateMenu();
    }

    //closes the shop menu
    public void BackToCar()
    {
        gameObject.SetActive(false);
    }

    //updates the shop menu
    public void UpdateMenu()
    {
        //update shop option availability
        if (inv.coinData.Quantity < 15 || stats.health>=100)//Not sure how to access max health, but that's the second check
        {
            increaseButton.interactable = false;
            increaseButton.GetComponent<Image>().color = Color.grey;

        } else if (inv.coinData.Quantity < 30)// ADD OR FROM PLAYER MAXHEALTH == CAP
        {
            restoreButton.interactable = false;
            increaseButton.interactable = false;
            restoreButton.GetComponent<Image>().color = Color.grey;
            increaseButton.GetComponent<Image>().color = Color.grey;
        }

        //update information displayed
        moneyDisplay.text = "Money: " + inv.coinData.Quantity.ToString();

        //ADD UPDATE BASED ON PLAYER HEALTH
    }
}
