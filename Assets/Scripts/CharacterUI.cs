using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    GridLayoutGroup upperGrid;//Grid Layout component of stats
    GridLayoutGroup statGrid;//Grid Layout component of stats

    RectTransform upperPanel;//RectTransform of the upper panel (image and stat grid)
    RectTransform image;//RectTransform of placeholder image of character


    InputField[] statFields;//InputField for the 6 character stats

    Text HP;
    Text SP;

    Vector2 tempVector;//used throughout code as temp variable


    delegate void Randoms();//delegate used to ramdomize all 6 stats
    Randoms randoms;
    

    void Start()
    {
        upperGrid = GameObject.FindWithTag("UpperPanel").GetComponentInChildren<GridLayoutGroup>();
        statGrid = GameObject.FindWithTag("StatPanel").GetComponentInChildren<GridLayoutGroup>();

        upperPanel = GameObject.FindWithTag("UpperPanel").GetComponentInChildren<RectTransform>();
        image = GameObject.FindWithTag("Image").GetComponent<RectTransform>();

        HP= GameObject.FindWithTag("HP").GetComponent<Text>();
        SP = GameObject.FindWithTag("SP").GetComponent<Text>();

        tempVector = new Vector2();

        //fetches the InputField's of the 6 stats
        GameObject[] temp = GameObject.FindGameObjectsWithTag("StatField");
        statFields = new InputField[temp.Length];
        float zero = 0f;
        for(int i = 0; i < temp.Length; i++)
        {
            statFields[i]=temp[i].GetComponent<InputField>();
            statFields[i].text = zero.ToString();
        }

        //add all 6 random stat functions to delegate
        randoms = randomStrength;
        randoms += randomToughness;
        randoms += randomDexterity;
        randoms += randomIQ;
        randoms += randomPower;
        randoms += randomCharm;

    }
    
    // Update is called once per frame
    void Update()
    {

        /********************
        *   Stat Update     *
        * *******************/

        //note negative stats are permitted (ex: debuffs)

        int tempF;

        //HP formula: Strength + Toughness + Power
        //if one of the stat fields is not parse-able (null), then set to -999
        int hp = int.TryParse(statFields[0].text, out tempF) && int.TryParse(statFields[1].text, out tempF) && int.TryParse(statFields[4].text, out tempF)
            ? int.Parse(statFields[0].text) + int.Parse(statFields[1].text) + int.Parse(statFields[4].text)
            : -999;
        HP.text = "HP: " + hp.ToString();

        //SP formula: Dexterity + IQ + Charm
        //if one of the stat fields is not parse-able (null), then set to -999
        int sp = int.TryParse(statFields[2].text, out tempF) && int.TryParse(statFields[3].text, out tempF) && int.TryParse(statFields[5].text, out tempF)
            ? int.Parse(statFields[2].text) + int.Parse(statFields[3].text) + int.Parse(statFields[5].text)
            : -999;
        SP.text = "SP: " + sp.ToString();

        /*********************
         * Horizontal Layout *
         * ******************/
        if (Screen.width > Screen.height)
        {

            //set upper grid to have 2 column
            upperGrid.constraintCount = 2;

            //change upper grid element size
            tempVector.x = 400;
            tempVector.y = 300;
            upperGrid.cellSize = tempVector;

            //resize upper panel
            tempVector.x = Screen.width;
            tempVector.y = 311.5f;
            upperPanel.sizeDelta = tempVector;

            //set stat grid to have 2 column
            statGrid.constraintCount = 2;

            //change stat grid element size
            tempVector.x = 200;
            tempVector.y = 80;
            statGrid.cellSize = tempVector;
        }
            

        /*******************
         * Vertical Layout *
         * ****************/

        else
        {
            //set upper grid to have 1 column
            upperGrid.constraintCount = 1;

            //change upper grid element size
            tempVector.x = 400;
            tempVector.y = 550;
            upperGrid.cellSize = tempVector;

            //resize upper panel
            tempVector.x = Screen.width;
            tempVector.y = 1350;
            upperPanel.sizeDelta = tempVector;

            //set stat grid to have 1 column
            statGrid.constraintCount = 1;

            //change stat grid element size
            tempVector.x = 400;
            tempVector.y = 80;
            statGrid.cellSize = tempVector;
        }

    }

    //set to public so it can be invoked on button click
    public void randomize()
    {

        randoms();
    }

    void randomStrength()
    {
        statFields[0].text = Random.Range(1, 10).ToString();
    }
    void randomToughness()
    {
        statFields[1].text = Random.Range(1, 10).ToString();
    }
    void randomDexterity()
    {
        statFields[2].text = Random.Range(1, 10).ToString();
    }
    void randomIQ()
    {
        statFields[3].text = Random.Range(1, 10).ToString();
    }
    void randomPower()
    {
        statFields[4].text = Random.Range(1, 10).ToString();
    }
    void randomCharm()
    {
        statFields[5].text = Random.Range(1, 10).ToString();
    }

}
