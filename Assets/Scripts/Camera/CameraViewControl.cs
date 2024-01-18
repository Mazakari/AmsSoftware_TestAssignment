using System;
using UnityEngine;

public class CameraViewControl : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Transform _thirdPersonView;
    [SerializeField] private Transform _topDownView;

    private void OnEnable()
    {
        SubscribeUiCallbacks();
        InitCamera();
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
    private void InitCamera()
    {
        CacheCamera();
        Activate3D();
    }
    private void CacheCamera() =>
       _camera = Camera.main;

    private void SwitchTo3D() => 
        Activate3D();
    private void SwitchTo2D() => 
        Activate2D();
    private void Activate3D()
    {
        try
        {
            _camera.transform.SetPositionAndRotation(_thirdPersonView.position, _thirdPersonView.rotation);

        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void Activate2D()
    {
        try
        {
            _camera.transform.SetPositionAndRotation(_topDownView.position, _topDownView.rotation);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
}
