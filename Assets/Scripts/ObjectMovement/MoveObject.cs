using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Collider _boundsCollider;
    private Collider[] _ownColliders;
    [SerializeField] private LayerMask _obstaclesLayers;
    private Camera _camera;
    private bool _selected = false;
    private Vector3 _startingPos;

    private void Start() => 
        InitObject();

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

        float checkRadius = _boundsCollider.bounds.size.magnitude;

        DeactivateOwnColliders();

        Collider[] collisions = new Collider[5];
        int hitColliders = Physics.OverlapSphereNonAlloc(transform.position, checkRadius, collisions, _obstaclesLayers);

        if (hitColliders > 0)
        {
            transform.position = _startingPos;
        }
        else
        {
            _startingPos = transform.position;
        }

        ActivateOwnColliders();
    }
    private void DeactivateOwnColliders()
    {
        if (_ownColliders.Length > 0)
        {
            for (int i = 0; i < _ownColliders.Length; i++)
            {
                _ownColliders[i].enabled = false;
            }
        }
    }
    private void ActivateOwnColliders()
    {
        if (_ownColliders.Length > 0)
        {
            for (int i = 0; i < _ownColliders.Length; i++)
            {
                _ownColliders[i].enabled = true;
            }
        }
    }

    private void InitObject()
    {
        _selected = false;
        _startingPos = transform.position;
        _camera = Camera.main;

        _ownColliders = GetComponentsInChildren<Collider>();
    }
}
