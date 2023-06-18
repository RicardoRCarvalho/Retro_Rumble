using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public GameObject powerPrefab;

    public void PowerAttack(bool Right)
    {
        GameObject powerObject = Instantiate(powerPrefab);
        powerObject.transform.position = transform.position;
        powerObject.transform.right = Right ? transform.right : transform.right * -1.0f;

    }
}
