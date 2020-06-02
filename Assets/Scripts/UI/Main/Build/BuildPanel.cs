using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject groundPlacementController;
    public GameObject hqPrefab;
    public GameObject branchPrefab;

    private GroundPlacementController gpcScript;

    private void Start() {
        gpcScript = groundPlacementController.GetComponent<GroundPlacementController>();
    }

    private void OnEnable() {
        Debug.Log("build panel active");
    }
    private void OnDisable() {
        Debug.Log("build panel inactive");
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnClickHQ() {
        gpcScript.SetCurrentPlaceableObject(hqPrefab);
    }
    public void OnClickBranch() {
        gpcScript.SetCurrentPlaceableObject(branchPrefab);
    }
}
