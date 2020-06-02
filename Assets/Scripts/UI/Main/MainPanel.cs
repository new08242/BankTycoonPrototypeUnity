using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public GameObject buildPanel;
  
    public void OnClickBuild() {
        gameObject.SetActive(false);
        buildPanel.SetActive(true);
    }
}
