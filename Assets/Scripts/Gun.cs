using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviourPun,IPunObservable
{
    [SerializeField] private Transform _bulletPos;
    private Vector3 _mousePos;
    private Vector3 _targetDirection;
    private Quaternion _networkRotation;
    private void Start()
    {
        
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Resources.Load<GameObject>("Bullet");
        return Instantiate(bullet);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            RotateGun();
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _networkRotation, Time.deltaTime*10);
        }
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
        if (photonView.IsMine)
        {
            GameObject bullet = CreateBullet();
            bullet.transform.position = _bulletPos.position;
            bullet.GetComponent<Bullet>().SetDirection(_targetDirection);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            _networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
