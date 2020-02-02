using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Movement Player1;
    public Movement Player2;

    bool hasPressedX1 = false;
    bool hasPressedX2 = false;

    public TextMeshProUGUI PressX1;
    public TextMeshProUGUI PressX2;




    private void Start()
    {
        //playerInput = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(hasPressedX1 == false && hasPressedX2 == true)
        {
            PressX1.text = "Waiting for the other player";
        }

        if (hasPressedX1 == true && hasPressedX2 == false)
        {
            PressX2.text = "Waiting for the other player";
        }


        if (hasPressedX1 == true && hasPressedX2 == true)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void StartPressed(int playerNum)
    {
        if (playerNum == 0)
        {
            hasPressedX1 = true;
        }
        else if (playerNum == 1)
        {
            hasPressedX2 = true;
        }
    }
}
