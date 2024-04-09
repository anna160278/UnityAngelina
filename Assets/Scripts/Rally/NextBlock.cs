using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlock: MonoBehaviour
{
    public bool isTouched;

    public void SetTouched(bool touched)
    {
        isTouched = touched;
    }

    public bool IsTouched()
    {
        return isTouched;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            SetTouched(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            SetTouched(false);
        }
    }
}
