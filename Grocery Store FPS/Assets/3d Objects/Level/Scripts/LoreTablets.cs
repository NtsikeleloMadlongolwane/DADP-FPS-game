using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreTablets : MonoBehaviour
{
    public FirstPersonControls FirstPersonControls;
    public GameObject tabletMessage;

    public void ReadTablet(bool isReading)
    {
        if (isReading)
        {
            tabletMessage.SetActive(true);
            Debug.Log("Player is reading tablet");
            return;
        }
        else
        {
            tabletMessage.SetActive(false);
            Debug.Log("Player is NOT reading tablet");
            return;
        }

    }
}
