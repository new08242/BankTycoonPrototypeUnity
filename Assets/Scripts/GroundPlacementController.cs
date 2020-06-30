using System;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    // [SerializeField]
    // private GameObject[] placeableObjectPrefabs;

    // private GameObject currentPlaceableObject;
    private GameObject currentPlaceableObject;
    public GameObject cannotPlaceBanner;

    private float mouseWheelRotation;
    // private int currentPrefabIndex = -1;

    private void Update()
    {
        // HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            // RotateFromMouseWheel();
            // ReleaseIfClicked();
        }
    }

    // private void HandleNewObjectHotkey()
    // {
    //     for (int i = 0; i < placeableObjectPrefabs.Length; i++)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
    //         {
    //             if (PressedKeyOfCurrentPrefab(i))
    //             {
    //                 Destroy(currentPlaceableObject);
    //                 currentPrefabIndex = -1;
    //             }
    //             else
    //             {
    //                 if (currentPlaceableObject != null)
    //                 {
    //                     Destroy(currentPlaceableObject);
    //                 }

    //                 currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
    //                 currentPrefabIndex = i;
    //             }

    //             break;
    //         }
    //     }
    // }

    // private bool PressedKeyOfCurrentPrefab(int i)
    // {
    //     return currentPlaceableObject != null && currentPrefabIndex == i;
    // }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceableObject placeableObjectScript = currentPlaceableObject.GetComponent<PlaceableObject>();
                if (placeableObjectScript.IsPlaceable() && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Owned"))
                {
                    placeableObjectScript.Purchase();
                    currentPlaceableObject = null;
                }
                else {
                    GameObject banner = Instantiate(cannotPlaceBanner);
                    banner.transform.SetParent(currentPlaceableObject.transform);
                    // Vector3 objPos = currentPlaceableObject.transform.position;
                    banner.transform.localPosition = new Vector3(0, 3, 0);
                }
            }
        }
    }

    // private void RotateFromMouseWheel()
    // {
    //     mouseWheelRotation += Input.mouseScrollDelta.y;
    //     currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    // }

    // private void ReleaseIfClicked()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         PlaceableObject placeableObjectScript = currentPlaceableObject.GetComponent<PlaceableObject>();
    //         if (placeableObjectScript.IsPlaceable())
    //         {
    //             placeableObjectScript.Purchase();
    //             currentPlaceableObject = null;
    //         }
    //     }
    // }

    public void SetCurrentPlaceableObject(GameObject placeableObject) {
        if (placeableObject == null) {
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
            return;
        }

        if (currentPlaceableObject != null) {
            Destroy(currentPlaceableObject);
            currentPlaceableObject = null;
        }
        currentPlaceableObject = Instantiate(placeableObject);
    }
    public GameObject GetCurrentPlaceableObject() {
        return currentPlaceableObject;
    }
}