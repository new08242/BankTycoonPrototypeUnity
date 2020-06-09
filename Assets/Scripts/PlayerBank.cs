using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBank : MonoBehaviour
{
    // Resources
    private float money;

    // Start is called before the first frame update
    void Start()
    {
        money = 2 * Mathf.Pow(10, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMoney() {
        return money;
    }

    public float AddMoney(float amount) {
        money += amount;
        return money;
    }
}
