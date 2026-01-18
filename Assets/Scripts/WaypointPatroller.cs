using UnityEngine;

public class WaypointPatroller : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 1f;

    [Header("Path Settings")]
    [SerializeField] private Transform _waypointsRoot;

    private const float ReachedPointSqrDistance = 0.01f;

    private Transform[] _waypoints;
    private int _currentIndex;

    private void Start()
    {
        if (_waypointsRoot == null)
        {
            Debug.LogError($"{nameof(WaypointPatroller)} on {name}: waypoints root is not assigned.", this);
            enabled = false;

            return;
        }   
        

        int count = _waypointsRoot.childCount;
        if (count == 0)
        {
            Debug.LogError($"{nameof(WaypointPatroller)} on {name}: waypoints root has no children.", this);
            enabled = false;

            return;
        } 

        _waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            _waypoints[i] = _waypointsRoot.GetChild(i);
        }
    }

    private void Update()
    {
        Transform targetPoint = _waypoints[_currentIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);

        if ((transform.position - targetPoint.position).sqrMagnitude < ReachedPointSqrDistance)
            MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        _currentIndex++;

        if (_currentIndex >= _waypoints.Length)
            _currentIndex = 0;

        Vector3 nextPosition = _waypoints[_currentIndex].position;
        Vector3 direction = (nextPosition - transform.position).normalized;

        if (direction != Vector3.zero)
            transform.forward = direction;
    }
}