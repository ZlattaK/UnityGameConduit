using UnityEngine;
using System.Collections;

public class WireConnection : MonoBehaviour
{
    public SphereReveal sphereReveal;

    private bool connected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (connected) return;

        if (other.CompareTag("WireEnd"))
        {
            connected = true;

            Debug.Log("WIRES CONNECTED");

            sphereReveal.StartReveal();
        }
    }
}