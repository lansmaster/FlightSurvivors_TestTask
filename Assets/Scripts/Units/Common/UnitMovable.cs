using UnityEngine;

public abstract class UnitMovable
{
    public abstract void Move(Vector3 direction, float speed);
    public abstract void Follow(Vector3 targetPosition, float speed, float pursuitDistance);
    public abstract void Rotate(Vector3 axis, float rotationSpeed);
    public abstract void RotateByMouse(Vector2 mousePosition);
}