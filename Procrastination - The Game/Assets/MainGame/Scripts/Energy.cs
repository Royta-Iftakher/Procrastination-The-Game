using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float startingEnergy;
    public float currentEnergy; //{get; private set;}
    public PlayerMovement player;

    private void Awake()
    {
        currentEnergy  = startingEnergy;
    }

    public void LoseEnergy(float _energy) {
        currentEnergy = Mathf.Clamp(currentEnergy - _energy, 0, startingEnergy);

        if(currentEnergy > 0) {
            player.Hurt();
        }
        else {
            player.Die();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)) {
            LoseEnergy(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            currentEnergy = startingEnergy;
            player.Restart();
        }
        if(player == null) {
            player = FindObjectOfType<PlayerMovement>();
        }
    }
}
