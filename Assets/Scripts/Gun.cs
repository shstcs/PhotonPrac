using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletPos;
    private Vector3 _mousePos;
    private Vector3 _targetDirection;
    private void Awake()
    {
        
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Resources.Load<GameObject>("Bullet");
        return Instantiate(bullet);
    }

    void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        _mousePos = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
        _targetDirection = _mousePos - transform.position;
        float angle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Shoot()
    {
        GameObject bullet = CreateBullet();
        bullet.transform.position = _bulletPos.position;
        bullet.GetComponent<Bullet>().SetDirection(_targetDirection);
    }
}
