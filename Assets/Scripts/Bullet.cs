using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 direction, float speed)
    {
        transform.up = direction;
        _rigidbody.velocity = direction * speed;

        Destroy(gameObject, _lifeTime);
    }
}