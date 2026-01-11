using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _shootInterval = 1f;

    [SerializeField] private GameObject _bulletPrefab;
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

            GameObject newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            if (newBullet.TryGetComponent(out Rigidbody bulletRigidbody))
            {
                bulletRigidbody.transform.up = direction;
                bulletRigidbody.velocity = direction * _bulletSpeed;
            }
            else
            {
                Debug.LogWarning($"{nameof(BulletShooter)} on {name}: bullet prefab has no Rigidbody component.", this);
            }

            yield return _shootWait;
        }
    }
}