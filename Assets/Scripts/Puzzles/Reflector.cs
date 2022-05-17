using UnityEngine;

public abstract class Reflector : MonoBehaviour, IInteractable
{
    [SerializeField] protected float[] stanceRotationValues;

    protected int stanceIndex = 0;
    public abstract void Interact();
}
