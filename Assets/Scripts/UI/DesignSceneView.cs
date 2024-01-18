using System;
using UnityEngine;

public class DesignSceneView : MonoBehaviour
{
    public static event Action OnSwitchTo3DEvent;
    public static event Action OnSwitchTo2DEvent;

    public static event Action OnAddHouseEvent;
    public static event Action OnAddHouseFenceEvent;
    public static event Action OnAddTreeEvent;

    public void SwitchTo3DView()
    {
        Debug.Log("SwitchTo3DView");
        OnSwitchTo3DEvent?.Invoke();
    }

    public void SwitchTo2DView()
    {
        Debug.Log("SwitchTo2DView");
        OnSwitchTo2DEvent?.Invoke();
    }

    public void AddHouse()
    {
        Debug.Log("Add house at random position");
        OnAddHouseEvent?.Invoke();
    }

    public void AddHouseFence()
    {
        Debug.Log("Add house fence");
        OnAddHouseFenceEvent?.Invoke();
    }

    public void AddTree()
    {
        Debug.Log("Add tree at random position");
        OnAddTreeEvent?.Invoke();
    }
}
