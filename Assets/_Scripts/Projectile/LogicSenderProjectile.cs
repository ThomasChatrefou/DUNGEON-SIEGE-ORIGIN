using NaughtyAttributes;
using UnityEngine;

public class LogicSenderProjectile : MonoBehaviour
{
    [Tag]
    [SerializeField] private string _targetTag;
    [Tag]
    [SerializeField] private string _senderTag;

    private BaseAbilitySO _logicToSend;
    private AbilityBlackboard _dataToSend;
    private Vector3 _direction;
    private float _speed;

    public void Shoot(BaseAbilitySO logicToSend, AbilityBlackboard dataToSend, Vector3 direction, float speed)
    {
        _logicToSend = logicToSend;
        _dataToSend = dataToSend;
        _direction = direction;
        _speed = speed;
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag(_senderTag)) return;
        if (other.transform.CompareTag(_targetTag))
        {
            _logicToSend.Use(ref _dataToSend);
        }
        Destroy(gameObject);
    }

    /* // Does targets have a rigidbody ?
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(_senderTag)) return;
        if (collision.transform.CompareTag(_targetTag))
        {
            _logicToSend.Use(ref _dataToSend);
        }
        Destroy(gameObject);
    }
    */
}
