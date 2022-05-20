using UnityEngine;

public class HorizontalReflector : Reflector
{
    public override void Interact()
    {
        stanceIndex = (stanceIndex + 1) % stanceRotationValues.Length;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler( new Vector3(transform.eulerAngles.x, stanceRotationValues[stanceIndex], transform.eulerAngles.z)));
    }
}
