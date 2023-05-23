using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Energy playerEnergy;
    [SerializeField] private Image totalenergyBar;
    [SerializeField] private Image currentenergyBar;
    // Start is called before the first frame update
    void Awake() {
        playerEnergy = FindObjectOfType<Energy>();
    }
    void Start()
    {
        totalenergyBar.fillAmount = playerEnergy.currentEnergy / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentenergyBar.fillAmount = playerEnergy.currentEnergy / 10;
    }
}
