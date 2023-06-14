using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Tag]
    [SerializeField] private string _targetTag = "Player";

    private int _damage = 1; // ça n'a rien a foutre la 
    private float _speed;
    private float _lifeTime;
    public Vector3 Destination;
    private Vector3 direction;
    
  public void Launch(float speed,float lifeTime)
    {
        _speed = speed;
        _lifeTime = lifeTime;
        direction = (Destination - transform.position).normalized;
        StartCoroutine(Projectile());
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += direction * _speed * Time.deltaTime;
        }
    }


    private void OnDisable()
    {
        _speed = 0f;
        Destination = Vector3.zero;
        transform.position = new Vector3(1000f, 1000f, 1000f);
        gameObject.SetActive(false);
    }
    IEnumerator Projectile()
    {
        
        yield return new WaitForSeconds(_lifeTime);
        ProjectilePool.Instance.ClearOneProjectile(this.gameObject);

       
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            ProjectilePool.Instance.ClearOneProjectile(this.gameObject);
            //DoDamage
            //Debug.Log("touché a distance");
            bool proHealth = other.TryGetComponent<ICharacterHealth>(out var health);
            if (proHealth)
            {
                health.TakeDamage(_damage);
            }
            

        }
    }
}
