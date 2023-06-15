using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LogicSenderProjectile : MonoBehaviour
{
    [Tag]
    [SerializeField] private string _targetTag;
    [Tag]
    [SerializeField] private string _senderTag;

    private Rigidbody _rigidbody;
    private BaseAbilitySO _logicToSend;
    private AbilityBlackboard _dataToSend;

    public void Shoot(BaseAbilitySO logicToSend, AbilityBlackboard dataToSend, Vector3 direction, float speed)
    {
        _logicToSend = logicToSend;
        _dataToSend = dataToSend;
        _rigidbody.velocity = speed * direction;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(_senderTag)) return;
        if (collision.transform.CompareTag(_targetTag))
        {
            _logicToSend.Use(ref _dataToSend);
        }
        Destroy(gameObject);
    }
}
