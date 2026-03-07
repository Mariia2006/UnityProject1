using UnityEngine;

public class LowCenterOfMass : MonoBehaviour
{
    public Transform centerOfMass;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }
}
