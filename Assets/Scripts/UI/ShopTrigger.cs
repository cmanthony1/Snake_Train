using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    /**
     * This script controlls a box that on trigger prompts the player to open the shop screen. 
     * **/

    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject shopPrompt;
    [SerializeField] public bool on;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Clear()
    {
        shopScreen.SetActive(false);
        shopPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(on == true && Input.GetKeyDown(KeyCode.F))
        {
            shopScreen.SetActive(true);
            shopScreen.GetComponent<ShopController>().UpdateMenu();
            on = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            on = true;
            shopPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            on = false;
            shopPrompt.SetActive(false);
        }
    }
}
