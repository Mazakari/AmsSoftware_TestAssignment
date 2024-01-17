using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Collider _collider;

    private Camera _camera;
    private bool _selected = false;
    private Vector3 _startingPos;

    private void Start()
    {
        _selected = false;
        _startingPos = transform.position;
        _camera = Camera.main;
    }

    //private void Update()
    //{
    //    if (_selected)
    //    {
    //        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            transform.position = hit.point + Vector3.up * transform.localScale.y / 2;
    //        }
    //    }
    //}

    //public void SetSelected()
    //{
    //    _selected = !_selected;

    //    if (_selected == false)
    //    {
    //        if (Physics.CheckSphere(transform.position, transform.localScale.y / 2.1f))
    //        {
    //            transform.position = _startingPos;
    //        }
    //        else
    //        {
    //            _startingPos = transform.position;
    //        }
    //    }
    //    _collider.enabled = !_collider.enabled;
    //}
    public void OnBeginDrag(PointerEventData eventData)
    {
        _selected = true;
    }

    public void OnDrag(PointerEventData eventData)
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

    public void OnEndDrag(PointerEventData eventData)
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
}
