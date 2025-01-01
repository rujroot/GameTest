using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEffect : MonoBehaviour
{
    [SerializeField]
    private float displayTime = 0.0f;

    private void Start()
    {
        if(displayTime != 0) StartCoroutine(DestroyEffect());
    }

    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(displayTime);
        Destroy(gameObject);
    }
}
