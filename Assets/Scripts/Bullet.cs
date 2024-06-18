using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifetime = 2f;
    private float _bulletSpeedModifier = 10f;
    private Vector3 _direction = Vector3.zero;

    private void Update()
    {
        if ( _lifetime > 0) _lifetime -= Time.deltaTime;
        else Destroy(gameObject);

        transform.position += _direction * _bulletSpeedModifier * Time.deltaTime;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                //TODO 대미지 구현
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Pillar"))
            {
                Destroy(gameObject);
                Debug.Log("Pillar");
            }
        }
    }

    public void SetDirection(Vector3 dir)
    {
        Vector2 direction = dir;

        _direction = direction.normalized;
    }
}
