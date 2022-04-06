using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFirer : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Vector2 firingDirection;
    public float firingSpeed;
    public string targetTag;

    public void FireProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
        ProjectileHelper newProjectileHelper = newProjectile.GetComponent<ProjectileHelper>();
        newProjectileHelper.Fire(firingDirection, firingSpeed, targetTag);
    }
}
