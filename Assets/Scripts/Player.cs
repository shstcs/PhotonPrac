using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rb;
    private Gun _gun;            //총 스크립트 만들기
    private Vector2 _curMoveInput;
    private float _moveModifier = 3f;

    private void Awake()
    {
        _player = gameObject;
        _rb = GetComponent<Rigidbody2D>();
        _gun = GetComponentInChildren<Gun>();
    }
    private void Update()
    {
        
        Camera.main.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, Camera.main.transform.localPosition.z);
        OnMove(_curMoveInput);
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
        //float xpos = dir.x * _moveModifier * Time.deltaTime;
        //float ypos = dir.y * _moveModifier * Time.deltaTime;

        //_player.transform.position = _player.transform.position + new Vector3(xpos, ypos, 0);
        _rb.velocity = dir * _moveModifier;
    }
}
