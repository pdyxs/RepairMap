using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCondition : MonoBehaviour
{
    public List<GameObject> WinningChoice;

    protected List<GameObject> PlayersChoice;

    public void Start()
    {
        PlayersChoice = new List<GameObject>();
        //make correct ammount of playerschoice
        for (int playerNumber = 0; playerNumber < WinningChoice.Count; playerNumber++)
        {
            PlayersChoice.Add(null);
        }
    }

    public void PlayerSelectedObject(int playerNum, GameObject selectedGameObject)
    {
        if(playerNum < PlayersChoice.Count)
        {
            PlayersChoice[playerNum] = selectedGameObject;
            Debug.Log(PlayersChoice[playerNum].name);
        }
    }

    public void Update()
    {
        bool everyoneHasChosen = true;

        //loop through every player
        for(int playerNumber = 0; playerNumber < WinningChoice.Count ; playerNumber++)
        {

            //check if we are not out of bounds
            if (playerNumber < PlayersChoice.Count)
            {
                //get chosen gameobject
                GameObject chosen = PlayersChoice[playerNumber];

                if(chosen == null)
                {
                    //player hasn't chosen yet
                    everyoneHasChosen = false;
                    break;
                }
            }
            else
            {
                Debug.LogError("More winningchoices.count > playerschoices.count");
                everyoneHasChosen = false;
                break;
            }
        }

        if (everyoneHasChosen == true)
        {
            //find if we have a winning choice combo

            bool isAllWinningChoice = true;

            //loop through every player
            for (int playerNumber = 0; playerNumber < WinningChoice.Count; playerNumber++)
            {
                if (WinningChoice[playerNumber] != PlayersChoice[playerNumber])
                {
                    isAllWinningChoice = false;
                    break;
                }
            }

            if (isAllWinningChoice == true)
            {
                Debug.Log("WIN WIN WIN WIN");
            }
            else
            {
                Debug.Log("Lost Lost Lost");
            }

            //clear playerchoice
            for (int playerNumber = 0; playerNumber < PlayersChoice.Count; playerNumber++)
            {
                PlayersChoice[playerNumber] = null;
            }
        }
    }
}
