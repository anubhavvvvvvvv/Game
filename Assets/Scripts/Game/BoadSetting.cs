using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoadSetting : MonoBehaviour
{
    [SerializeField] private GameObject yellow_blue_board;
    [SerializeField] private GameObject red_green_board;

    [SerializeField] private GameObject[] player1;//yellow
    [SerializeField] private GameObject[] player2;//red
    [SerializeField] private GameObject[] player3;//blue
    [SerializeField] private GameObject[] player4;//green

    [SerializeField] private Sprite[] gotiColor;//yellow->red->blue->green
    [SerializeField] private Sprite[] boardColor;

    public int color = 2;

    private void Start()
    {
        color = PlayerPrefs.GetInt("BoardColor");
        if (color == 2) RedAsMyPlayer();
        else if (color == 3) BlueAsMyPlayer();
        else if (color == 4) GreenAsMyPlayer();
    }


    public void ChangeColor(int index, GameObject[] player)
    {
        for(int j = 0; j < 4; j++)
        {
            player[j].transform.GetComponent<Image>().sprite = gotiColor[index];
        }
    }

    public void BlueAsMyPlayer()
    {
       // yellow_blue_board.SetActive(true);
        //red_green_board.SetActive(false);
        red_green_board.GetComponent<Image>().sprite=boardColor[1];
       // yellow_blue_board.transform.Rotate(0, 0, 180);
        ChangeColor(2, player1);
        ChangeColor(3, player2);
        ChangeColor(0, player3);
        ChangeColor(1, player4);
    }
    public void RedAsMyPlayer()
    {
        //yellow_blue_board.SetActive(false);
        //red_green_board.SetActive(true);

        //red_green_board.transform.Rotate(0, 0, 180);
        red_green_board.GetComponent<Image>().sprite=boardColor[0];

        ChangeColor(1, player1);
        ChangeColor(2, player2);
        ChangeColor(3, player3);
        ChangeColor(0, player4);
    }
    public void GreenAsMyPlayer()
    {
        //yellow_blue_board.SetActive(false);
        //red_green_board.SetActive(true);
        red_green_board.GetComponent<Image>().sprite=boardColor[2];
        ChangeColor(3, player1);
        ChangeColor(0, player2);
        ChangeColor(1, player3);
        ChangeColor(2, player4);
    }
}
