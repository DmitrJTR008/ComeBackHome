using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoveComponent
{
    RocketController _mainRocket;
    private Joystick _moveJoystick, _rotateJoystick;
    private Rigidbody _rb;
    private Vector2 MoveRight;
    public RocketMoveComponent(RocketController rocket,Joystick move, Joystick rotate, Rigidbody rb) 
    {
        _mainRocket = rocket;
        _moveJoystick = move;
        _rotateJoystick = rotate;
        _rb = rb;
    }
   
    public void MoveRocket(float force, float maxVelocity)
    {
        float accelerate = _moveJoystick.Vertical + Input.GetAxisRaw("Vertical") > 0 ? _moveJoystick.Vertical + Input.GetAxisRaw("Vertical") : 0;
        _rb.AddForce(_mainRocket.transform.up * (accelerate * force * Time.fixedDeltaTime), ForceMode.Acceleration);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocity);
        if(_rb.velocity.y < -.1f && Mathf.Approximately(accelerate,0))
        {
            _mainRocket._currentState = RocketCurrentState.Drop;
        }
        else
            _mainRocket._currentState = Mathf.Approximately(accelerate,0)? RocketCurrentState.Stay : RocketCurrentState.Move;
    }
    public void RotateRocket()
    {
        MoveRight = new Vector2(_rotateJoystick.Direction.x + Input.GetAxisRaw("Horizontal"), _rotateJoystick.Direction.y + Input.GetAxis("Horizontal"));
        
        if (_rb.velocity.sqrMagnitude < .1f) return;
        if (MoveRight == Vector2.zero) return;
        float angle = -Mathf.Atan2(_rotateJoystick.Horizontal + Input.GetAxis("Horizontal"), _rotateJoystick.Vertical + Input.GetAxis("Vertical")) * Mathf.Rad2Deg;

        Quaternion rotate =  Quaternion.AngleAxis(angle, Vector3.forward);

        Quaternion lerp = Quaternion.Lerp(_mainRocket.transform.rotation, rotate, 2  * Time.deltaTime);
        _mainRocket.transform.localRotation = lerp;

    }
}
