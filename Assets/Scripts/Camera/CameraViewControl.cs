using System;
using UnityEngine;

public class CameraViewControl : MonoBehaviour
{
    [SerializeField] private Camera _firstPersonCamera;
    [SerializeField] private Camera _topDownCamera;

    public static event Action <Camera> OnCameraChangedEvent;

    private void OnEnable()
    {
        SubscribeUiCallbacks();

        InitCameras();
    }

    private void OnDisable() => 
        UnsubscribeUiCallbacks();

    private void SubscribeUiCallbacks()
    {
        DesignSceneView.OnSwitchTo3DEvent += SwitchTo3D;
        DesignSceneView.OnSwitchTo2DEvent += SwitchTo2D;
    }
    private void UnsubscribeUiCallbacks()
    {
        DesignSceneView.OnSwitchTo3DEvent -= SwitchTo3D;
        DesignSceneView.OnSwitchTo2DEvent -= SwitchTo2D;
    }

    private void SwitchTo3D() => 
        Activate3D();
    private void SwitchTo2D() => 
        Activate2D();
   
    private void InitCameras() => 
        Activate3D();

    private void Activate3D()
    {
        try
        {
            _firstPersonCamera.gameObject.SetActive(true);
            _topDownCamera.gameObject.SetActive(false);

            OnCameraChangedEvent?.Invoke(_firstPersonCamera);
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }

    }
    private void Activate2D()
    {
        try
        {
            _firstPersonCamera.gameObject.SetActive(false);
            _topDownCamera.gameObject.SetActive(true);

            OnCameraChangedEvent?.Invoke(_topDownCamera);
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
}
