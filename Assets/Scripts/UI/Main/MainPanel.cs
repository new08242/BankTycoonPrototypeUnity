using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public GameObject buildPanel;
    public GameObject productPanel;
    public GameObject hrPanel;
    public GameObject researchPanel;
    public GameObject pausePanel;
  
    public void OnClickBuild() {
        gameObject.SetActive(false);
        buildPanel.SetActive(true);
    }

    public void OnClickProduct() {
        gameObject.SetActive(false);
        productPanel.SetActive(true);
        Bank.Instance.SetMouseState(MouseState.UIControl);
    }

    public void OnClickHR() {
        gameObject.SetActive(false);
        hrPanel.SetActive(true);
        Bank.Instance.SetMouseState(MouseState.UIControl);
    }
    public void CloseHR() {
        gameObject.SetActive(true);
        hrPanel.SetActive(false);
        Bank.Instance.SetMouseState(MouseState.CameraControl);
    }

    public void OnClickResearch() {
        gameObject.SetActive(false);
        researchPanel.SetActive(true);
        Bank.Instance.SetMouseState(MouseState.UIControl);
    }
    public void CloseResearch() {
        gameObject.SetActive(true);
        researchPanel.SetActive(false);
        Bank.Instance.SetMouseState(MouseState.CameraControl);
    }
}
