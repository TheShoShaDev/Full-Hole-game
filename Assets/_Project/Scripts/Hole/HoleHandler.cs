using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleHandler : MonoBehaviour
{
    [SerializeField] private int NormalSphereLayer;
    [SerializeField] private int FallingSphereLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == NormalSphereLayer)
        {
            other.gameObject.layer = FallingSphereLayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == FallingSphereLayer)
        {
            other.gameObject.layer = NormalSphereLayer;
        }
    }

}
