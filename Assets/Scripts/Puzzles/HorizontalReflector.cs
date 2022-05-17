using UnityEngine;

public class HorizontalReflector : Reflector
{
    public override void Interact()
    {
        stanceIndex = (stanceIndex + 1) % stanceRotationValues.Length;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler( new Vector3(0, stanceRotationValues[stanceIndex], 0)));
    }
}
