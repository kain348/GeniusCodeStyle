using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _shootInterval = 1f;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _target;

    private Coroutine _shootingCoroutine;
    private WaitForSeconds _shootWait;

    private void Start()
    {
        if (_bulletPrefab == null)
        {
            Debug.LogError($"{nameof(BulletShooter)} on {name}: bullet prefab is not assigned.", this);
            enabled = false;

            return;
        }

        if (_target == null)
        {
            Debug.LogError($"{nameof(BulletShooter)} on {name}: target is not assigned.", this);
            enabled = false;

            return;
        }

        _shootWait = new WaitForSeconds(_shootInterval);
        _shootingCoroutine = StartCoroutine(ShootingRoutine());
    }

    private void OnDisable()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
        }
    }

    private IEnumerator ShootingRoutine()
    {
        yield return null;

        while (true)
        {
            Vector3 direction = (_target.position - transform.position).normalized;

            Bullet newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            newBullet.Initialize(direction, _bulletSpeed);

            yield return _shootWait;
        }
    }
}