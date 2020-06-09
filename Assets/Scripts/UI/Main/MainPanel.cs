using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public GameObject buildPanel;
    public GameObject productPanel;
  
    public void OnClickBuild() {
        gameObject.SetActive(false);
        buildPanel.SetActive(true);
    }

    public void OnClickProduct() {
        gameObject.SetActive(false);
        productPanel.SetActive(true);
    }
}
