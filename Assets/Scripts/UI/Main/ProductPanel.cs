using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    public GameObject mainPanel;
    
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
        mainPanel.SetActive(true);
    }
}
