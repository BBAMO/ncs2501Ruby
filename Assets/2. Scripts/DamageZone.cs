using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        //RubyController controller = other.GetComponent<RubyController>();
        //if (controller != null)

        if (other.TryGetComponent<RubyController>(out var controller))
        {
            controller.ChangeHealth(-1);
        }
    }
}
