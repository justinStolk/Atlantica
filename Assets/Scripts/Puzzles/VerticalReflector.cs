using UnityEngine;

public class VerticalReflector : Reflector
{
    public override void Interact()
    {
        stanceIndex = (stanceIndex + 1) % stanceRotationValues.Length;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(stanceRotationValues[stanceIndex], 0, 0)));
    }
}
