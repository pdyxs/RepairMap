using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDown : MonoBehaviour
{
    protected WinningCondition winningCondition;
    public Camera cameraUsed;
    
    public void Start()
    {
        winningCondition = FindObjectOfType<WinningCondition>();

        if(winningCondition == null)
        {
            Debug.LogError("Couldn't find winning condition in mousedown");
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseInsideViewport(cameraUsed) == true)
            {
                Ray ray = cameraUsed.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    BaseMapObject mapObject = hit.collider.gameObject.GetComponent<BaseMapObject>();
                    PlayerLabel playerLabel = cameraUsed.GetComponent<PlayerLabel>();
                    if (mapObject != null)
                    {
                        winningCondition.PlayerSelectedObject(playerLabel.PlayerNumber, hit.collider.gameObject);
                    }
                }
            }
        }
    }


    bool mouseInsideViewport(Camera local_cam)
    {
        //if (!Input.mousePresent) return true; //always true if no mouse??

        //Vector3 mouseViewPos = local_cam.ScreenToViewportPoint(Input.mousePosition);

        return local_cam.pixelRect.Contains(Input.mousePosition);
    }
}
