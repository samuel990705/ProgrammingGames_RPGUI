using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    GridLayoutGroup invGrid;//Grid Layout component of inventory


    RectTransform invPanel;//RectTransform of inventory panel
    RectTransform buttonsPanel;
    RectTransform canvas;

    Item[] items;

    Text[] nameTexts;
    Text[] priceTexts;
    Text[] weightTexts;
    Text[] damageTexts;

    Vector2 tempV;

    GameObject[] tempObject;

    void Start()
    {
        //fetches references using tags
        canvas= GameObject.FindWithTag("Canvas").GetComponentInChildren<RectTransform>();
        buttonsPanel= GameObject.FindWithTag("ButtonsPanel").GetComponentInChildren<RectTransform>();
        tempV = new Vector2();
        invGrid= GameObject.FindWithTag("InvPanel").GetComponentInChildren<GridLayoutGroup>();
        invPanel= GameObject.FindWithTag("InvPanel").GetComponentInChildren<RectTransform>();

        
        //stores references to all the text fields of all 16 items
        tempObject = GameObject.FindGameObjectsWithTag("Name");
        nameTexts = new Text[tempObject.Length];
        for (int i=0;i< tempObject.Length; i++)
        {
            nameTexts[i] = tempObject[i].GetComponent<Text>();
        }

        tempObject = GameObject.FindGameObjectsWithTag("Price");
        priceTexts = new Text[tempObject.Length];
        for (int i = 0; i < tempObject.Length; i++)
        {
            priceTexts[i] = tempObject[i].GetComponent<Text>();
        }

        tempObject = GameObject.FindGameObjectsWithTag("Weight");
        weightTexts = new Text[tempObject.Length];
        for (int i = 0; i < tempObject.Length; i++)
        {
            weightTexts[i] = tempObject[i].GetComponent<Text>();
        }

        tempObject = GameObject.FindGameObjectsWithTag("Damage");
        damageTexts = new Text[tempObject.Length];
        for (int i = 0; i < tempObject.Length; i++)
        {
            damageTexts[i] = tempObject[i].GetComponent<Text>();
        }

        //initialize and assign initial values for items (using values in unity editor)
        items = new Item[tempObject.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new Item(nameTexts[i].text, float.Parse(priceTexts[i].text), float.Parse(weightTexts[i].text), float.Parse(damageTexts[i].text));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //display item details for all 16 items
        for(int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i].text = items[i].name;
            priceTexts[i].text = "$"+items[i].price.ToString();
            weightTexts[i].text = items[i].weight.ToString()+"kg";
            damageTexts[i].text = items[i].damage.ToString() + " dmg";
        }

        //adjust button panel
        tempV.x = buttonsPanel.sizeDelta.x;
        tempV.y = canvas.sizeDelta.y*0.15f;
        buttonsPanel.sizeDelta = tempV;//update size

        //adjust inventory panel
        tempV = canvas.sizeDelta;
        tempV.y *= 0.7f;
        invPanel.sizeDelta = tempV;//update size

        tempV.x = 0;
        tempV.y= canvas.sizeDelta.y * -0.35f;
        invPanel.localPosition = tempV;//update position


        /*********************
         * Horizontal Layout *
         * ******************/
        if (Screen.width > Screen.height)
        {

            //set inventory grid to have 8 column
            invGrid.constraintCount = 8;
        }


        /*******************
         * Vertical Layout *
         * ****************/

        else
        {
            //set inventory grid to have 4 column
            invGrid.constraintCount = 4;
        }

        //scales cell size
        tempV.x = invPanel.sizeDelta.x / invGrid.constraintCount;
        tempV.y = invPanel.sizeDelta.y / (16 / invGrid.constraintCount);//there are 16 cells
        invGrid.cellSize = tempV;

    }


    //4 sort functions that uses LINQ
    public void sortByName()
    {
        //sort objects using LINQ
        Item[] sorted = items.OrderBy(i => i.name).ToArray();
        items = sorted;//set original array to sorted array
    }
    public void sortByPrice()
    {
        //sort objects using LINQ
        Item[] sorted = items.OrderBy(i => i.price).ToArray();
        items = sorted;//set original array to sorted array
    }
    public void sortByWeight()
    {
        //sort objects using LINQ
        Item[] sorted = items.OrderBy(i => i.weight).ToArray();
        items = sorted;//set original array to sorted array
    }
    public void sortByDamage()
    {
        //sort objects using LINQ
        Item[] sorted = items.OrderBy(i => i.damage).ToArray();
        items = sorted;//set original array to sorted array
    }
}


//Item object definition 
public class Item
{
    public string name;
    public float weight;
    public float price;
    public float damage;

    //constructor
    public Item(string name, float price, float weight, float damage)
    {
        this.name = name;
        this.weight = weight;
        this.price = price;
        this.damage = damage;
    }


}

//name
//price
//weight
//damage