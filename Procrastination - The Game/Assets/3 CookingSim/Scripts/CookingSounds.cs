using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click() {
        AudioManager.instance.KitchenButtonClick();
    }
}
