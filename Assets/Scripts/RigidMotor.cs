using UnityEngine;

[AddComponentMenu("Player/RigidMotor")]
[RequireComponent(typeof(Rigidbody))]
public class RigidMotor : MonoBehaviour
{
    public float Speed = 0.001f;

    private Transform _transform = null;
    public new Transform transform
    {
        get
        {
            if (_transform == null) _transform = GetComponent<Transform>();
            return _transform;
        }
    }

    private Rigidbody _rigidbody = null;
    public new Rigidbody rigidbody
    {
        get
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            return _rigidbody;
        }
    }

    private void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;
		if (!SixenseInput.Controllers[1].GetButton( SixenseButtons.BUMPER ))
        {
			moveDirection = new Vector3(SixenseInput.Controllers[0].JoystickX, 0, SixenseInput.Controllers[0].JoystickY);
			transform.Rotate(new Vector3(-SixenseInput.Controllers[1].JoystickY,SixenseInput.Controllers[1].JoystickX ,0));
        }

        Vector3 targetVelocity = transform.TransformDirection(moveDirection) * Speed;
        Vector3 changeVelocity = targetVelocity - rigidbody.velocity;
        changeVelocity.y = 0f;

        float deltaVelocity = changeVelocity.magnitude;
        if (deltaVelocity > Speed)
        {
            changeVelocity = changeVelocity / deltaVelocity * Speed;
        }

        rigidbody.AddForce(changeVelocity, ForceMode.VelocityChange);

		if (SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.BUMPER))
			transform.position += Vector3.up;
		
		if (SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.TRIGGER))
			transform.position -= Vector3.up;

		// Cancel Z rotation.
		float z = transform.eulerAngles.z;
		transform.Rotate (0, 0, -z);
    }
}
