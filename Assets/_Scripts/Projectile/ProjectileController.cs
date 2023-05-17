using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _speed;
    public Vector3 Destination;
    private Vector3 direction;
    
  public void Launch()
    {
        _speed = 5f;
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
        
        yield return new WaitForSeconds(5f);
        ProjectilePool.Instance.ClearOneProjectile(this.gameObject);

       
        yield return null;
    }
}
