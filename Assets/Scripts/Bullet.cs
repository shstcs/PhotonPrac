using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun, IPunObservable
{
    private float _lifetime = 2f;
    private float _bulletSpeedModifier = 10f;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _networkPos;

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (_lifetime > 0) _lifetime -= Time.deltaTime;
            else Destroy(gameObject);

            transform.position += _direction * _bulletSpeedModifier * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _networkPos, Time.deltaTime);
        }
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            _networkPos = (Vector3)stream.ReceiveNext();
        }
    }
}
