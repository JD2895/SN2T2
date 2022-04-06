using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHelper : MonoBehaviour
{
    public GameObject explosion;
    public GameObject projectileSprite;
    LayerMask projectileLayer;
    bool isFired;
    Rigidbody2D rb;
    string targetTag = "none";

    public void Fire(Vector2 direction, float projectileSpeed, string newTargetTag)
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
        targetTag = newTargetTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            projectileSprite.SetActive(false);
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Instantiate(explosion, this.transform);
            Destroy(this.gameObject, 5f);
        }

    }
}
