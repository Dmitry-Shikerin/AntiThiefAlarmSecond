using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action Entered;
    public event Action Exitered;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            Entered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Exitered?.Invoke();
        }
    }
}
