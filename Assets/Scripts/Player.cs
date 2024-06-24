using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody2D _rb;
    private Gun _gun;            //총 스크립트 만들기
    private Vector2 _curMoveInput;
    private float _moveModifier = 3f;
    private Vector3 _networkPos;
    private Quaternion _networkRot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gun = GetComponentInChildren<Gun>();
    }

    private void Start()
    {
        if(!photonView.IsMine)
        {
            _networkPos = transform.position;
            _networkRot = transform.rotation;
            gameObject.tag = "Enemy";
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.localPosition.z);
            OnMove(_curMoveInput);
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position,_networkPos,Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation,_networkRot,Time.deltaTime);
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)    
        {
            _curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMoveInput = Vector2.zero;
        }
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _gun.Shoot();
        }
    }

    private void OnMove(Vector2 dir)
    {
        _rb.velocity = dir * _moveModifier;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
