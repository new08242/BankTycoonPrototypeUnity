using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractManager : MonoBehaviour
{
    public GameObject content;
    public GameObject contractPrefab;
    public GameObject baseContractGObj;

    // Content
    private Vector2 baseContentSize = new Vector2(0, 285);

    // Contract
    private Vector2 anMin = new Vector2(0, 0.5f);
    private Vector2 anMax = new Vector2(0, 0.5f);
    private Vector2 anPivot = new Vector2(0.5f, 0.5f);
    private Vector2 contractSize = new Vector2(150, 200);
    private Vector3 startPos;
    // Singleton instance
    public static ContractManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        startPos = baseContractGObj.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateContractList();
    }

    void UpdateContractList() {
        if (Bank.Instance.loans.Count < 1) {
            return;
        }

        // Set content size
        Vector2 contentSize = baseContentSize;
        if (Bank.Instance.loans.Count < 4 ) {
            RectTransform rectTrans = content.GetComponent<RectTransform>();
            rectTrans.sizeDelta = contentSize;

        } else {
            RectTransform rectTrans = content.GetComponent<RectTransform>();
            contentSize.x += 100;

            int extraX = (200 * (Bank.Instance.loans.Count-4));
            contentSize.x += extraX; 
            rectTrans.sizeDelta = contentSize;
        }

        // Set contracts
        Vector3 pos = startPos;
        foreach (var contract in Bank.Instance.loans) {
            RectTransform rectTrans = contract.contractUI.GetComponent<RectTransform>();
            // set anchor
            rectTrans.anchorMin = anMin;
            rectTrans.anchorMax = anMax;
            rectTrans.pivot = anPivot;

            // set size
            rectTrans.sizeDelta = contractSize;

            // set position
            rectTrans.localPosition = pos;

            // set next start pos;
            pos.x += 200;

            // set detail to contract
            Text contractText = contract.contractUI.transform.GetChild(0).GetComponent<Text>();
            
        }
    }

    public GameObject CreateContractUI() {
        GameObject c = Instantiate(contractPrefab);
        c.transform.SetParent(content.transform, false);
        c.transform.localPosition = startPos;

        return c;
    }
}
