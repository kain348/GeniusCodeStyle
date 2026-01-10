using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform ObjectToShoot;

    private bool _isWork = true;
    private Coroutine _shootingWorkerRoutine;
    private WaitForSeconds _shootingWait;

    private void Start()
    {
        _shootingWait = new WaitForSeconds(_timeWaitShooting);
        _shootingWorkerRoutine = StartCoroutine(ShootingWorkerRoutine());
    }

    private void OnDisable()
    {
        _isWork = false;

        if (_shootingWorkerRoutine != null)
        {
            StopCoroutine(_shootingWorkerRoutine);
            _shootingWorkerRoutine = null;
        }
    }

    private IEnumerator ShootingWorkerRoutine()
    {
        yield return null;

        while (_isWork)
        {
            if (ObjectToShoot == null)
                throw new NullReferenceException(nameof(IndexPlaces));

            if(_prefab == null)
                throw new NullReferenceException(nameof(_prefab));

            var _vector3Direction = (ObjectToShoot.position - transform.position).normalized;
            var NewBullet = Instantiate(_prefab, transform.position + _vector3Direction, Quaternion.identity);

            NewBullet.GetComponent<Rigidbody>().transform.up = _vector3direction;
            NewBullet.GetComponent<Rigidbody>().velocity = _vector3direction * _speed;

            yield return _shootingWait;
        }
    }
}