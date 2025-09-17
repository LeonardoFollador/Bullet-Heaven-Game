using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{
    public float rotateSpeed;

    public Transform holder;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
}
