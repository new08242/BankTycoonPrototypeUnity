﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanManagePanel : MonoBehaviour
{
    public GameObject productMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
        productMenuPanel.SetActive(true);
    }
}
