using UnityEngine;
public class PhysicalAreaOfThePlayingField : MonoBehaviour
{
    public delegate void BorderDelegate(Collider obj);
    public event BorderDelegate objectLeavingThePlayingField;
    private void OnTriggerExit(Collider other)
    {
        objectLeavingThePlayingField?.Invoke(other);
    }    
}
