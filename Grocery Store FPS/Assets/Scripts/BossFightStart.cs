using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public MonoBehaviour spikewavescript;
    public GameObject seal;
    public GameObject boss;
    public ButtonHandler ButtonHandler;
    // Update is called once per frame
    void Update()
    {
        if (seal == null)
        {
            spikewavescript.enabled = true;
        }

        if(boss == null) 
        {
            ButtonHandler.Win();
        }
    }
}
