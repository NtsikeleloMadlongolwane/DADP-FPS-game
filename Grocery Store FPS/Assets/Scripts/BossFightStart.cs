using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public MonoBehaviour spikewavescript;
    public GameObject seal;
    public GameObject boss;
    public ButtonHandler ButtonHandler;
    public GameObject roof;
    // Update is called once per frame
    void Update()
    {
        if (seal == null)
        {
            spikewavescript.enabled = true;
            roof.SetActive(false);
        }

        if(boss == null) 
        {
            ButtonHandler.Win();
        }
    }
}
