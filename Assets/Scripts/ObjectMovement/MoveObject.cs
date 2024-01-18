using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Collider _collider;

    private Camera _camera;
    private bool _selected = false;
    private Vector3 _startingPos;

    private void OnEnable() => 
        SubscribeCameraCallbacks();

    private void Start() => 
        InitObject();

    private void OnDisable() => 
        UnsubscribeCameraCallbacks();
  

    public void OnBeginDrag(PointerEventData eventData) => 
        _selected = true;

    public void OnDrag(PointerEventData eventData) => 
        DragObject();

    public void OnEndDrag(PointerEventData eventData) => 
        PlaceObject();

    private void DragObject()
    {
        if (_selected)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPos = hit.point;
                newPos.y = transform.position.y;

                transform.position = newPos;
            }
        }
    }
    private void PlaceObject()
    {
        _selected = false;

        float checkRadius = _collider.bounds.size.magnitude / 2.5f;
        _collider.enabled = false;

        Collider[] collisions = new Collider[1];
        int hitColliders = Physics.OverlapSphereNonAlloc(transform.position, checkRadius, collisions);

        if (hitColliders > 0)
        {
            transform.position = _startingPos;
        }
        else
        {
            _startingPos = transform.position;
        }

        _collider.enabled = true;
    }

    private void SwitchActiveCamera(Camera newActiveCamera) =>
       _camera = newActiveCamera;

    private void InitObject()
    {
        _selected = false;
        _startingPos = transform.position;
        _camera = Camera.main;
    }
    private void SubscribeCameraCallbacks() =>
       CameraViewControl.OnCameraChangedEvent += SwitchActiveCamera;
    private void UnsubscribeCameraCallbacks() =>
      CameraViewControl.OnCameraChangedEvent -= SwitchActiveCamera;
}
