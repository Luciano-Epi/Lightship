using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR.NavigationMesh;
using UnityEngine.InputSystem;

/// SUMMARY:
/// LightshipNavMeshSample
/// This sample shows how to use LightshipNavMesh to add user driven point and click navigation.
/// When you first touch the screen, it will place your agent prefab.
/// Tapping a location moves the agent to that location.
/// The toggle button shows/hides the navigation mesh and path.
/// It assumes the _agentPrefab has LightshipNavMeshAgent on it.
/// If you have written your own agent type, either swap yours in or inherit from it.
///
public class NavMeshHowTo : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LightshipNavMeshManager _navmeshManager;

    [SerializeField]
    private GameObject _agentPrefab;

    private GameObject _creature;
    private LightshipNavMeshAgent _agent;

    void Update()
    {
        HandleTouch();
    }

    public void ToggleVisualisation()
    {
        //turn off the rendering for the navmesh
        _navmeshManager.GetComponent<LightshipNavMeshRenderer>().enabled =
            !_navmeshManager.GetComponent<LightshipNavMeshRenderer>().enabled;

        //turn off the path rendering on any agent
        _agent.GetComponent<LightshipNavMeshAgentPathRenderer>().enabled =
            !_agent.GetComponent<LightshipNavMeshAgentPathRenderer>().enabled;
    }

    private void HandleTouch()
    {
        //in the editor we want to use mouse clicks, on phones we want touches.
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
#else
        //if there is a touch, call our function
        if (Input.touchCount <= 0)
            return;

        var touch = Input.GetTouch(0);

        //if there is no touch or touch selects UI element
        if (Input.touchCount <= 0 )
            return;
        if (touch.phase == UnityEngine.TouchPhase.Began)
#endif
        {
#if UNITY_EDITOR
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
#else
            Ray ray = _camera.ScreenPointToRay(touch.position);
#endif
            //project the touch point from screen space into 3d and pass that to your agent as a destination
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (_creature == null)
                {
                    _creature = Instantiate(_agentPrefab);
                    _creature.transform.position = hit.point;
                    _agent = _creature.GetComponent<LightshipNavMeshAgent>();
                }
                else
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }
    }
}