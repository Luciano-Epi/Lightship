using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PortalPlacement : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject placementObject;

    private bool isInstantiated = false;
    // Update is called once per frame
    void Update()
    {

#if UNITY_IOS || UNITY_ANDROID
        
        if (Input.touchCount > 0 && Input.touchCount < 2 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = touch.position;

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0) {
                // We hit a UI element
                Debug.Log("We hit an UI Element");
                return;
            }
            
            Debug.Log("Touch detected, fingerId: " + touch.fingerId);  // Debugging line


            TouchToRay(touch.position);
        }
#endif
    }
    
    void TouchToRay(Vector3 touch)
    {
        Ray ray = mainCam.ScreenPointToRay(touch);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (!isInstantiated)
            {
                // Si no hay una instancia existente, crea una nueva
                placementObject = Instantiate(placementObject, hit.point, Quaternion.FromToRotation(transform.up, hit.normal));
                isInstantiated = true;
            }
            else
            {
                // Si ya hay una instancia existente, actualiza su posición
                placementObject.transform.position = hit.point;
                // También puedes actualizar su rotación si es necesario
                placementObject.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);
            }
        }
    }
}

