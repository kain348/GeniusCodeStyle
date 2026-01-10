using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private float number;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private GameObject _prefab;

    public Transform ObjectToShoot;

    void Start()
    {
        StartCoroutine(_shootingWorker());
    }

    public  IEnumerator _shootingWorker()
    {
        public bool isWork = enabled;

        while (isWork)
        {
            var _vector3direction = (ObjectToShoot.position - transform.position).normalized;
            var NewBullet = Instantiate(_prefab, transform.position + _vector3direction, Quaternion.identity);

            NewBullet.GetComponent<Rigidbody>().transform.up = _vector3direction;
            NewBullet.GetComponent<Rigidbody>().velocity = _vector3direction * number;

            yield return new WaitForSeconds(_timeWaitShooting);
        }
    }
}