using UnityEngine;

public class TransformUnitMovable : UnitMovable
{
    private Transform _transform;

    public TransformUnitMovable(Transform transform)
    {
        _transform = transform;
    }

    public override void Move(Vector3 direction, float speed)
    {
        _transform.Translate(direction * speed * Time.deltaTime);
    }

    public override void Follow(Vector3 targetPosition, float speed, float pursuitDistance)
    {
        if(Vector2.Distance(_transform.position, targetPosition) > pursuitDistance)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public override void Rotate(Vector3 axis, float rotationSpeed)
    {
        _transform.Rotate(axis, rotationSpeed * Time.deltaTime);
    }

    public override void RotateByMouse(Vector2 mousePosition)
    {
        Vector2 unitPos = _transform.position;
        Vector2 direction = mousePosition - unitPos;
        _transform.up = direction;
    }
}
