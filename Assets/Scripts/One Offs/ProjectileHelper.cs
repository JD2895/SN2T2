using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHelper : MonoBehaviour
{
    public GameObject explosion;
    public GameObject projectileSprite;
    public ParticleSystem p_Sys;
    bool isFired;
    Rigidbody2D rb;
    string targetTag = "none";
    string targetTag2 = "none";

    public void Fire(Vector2 direction, float projectileSpeed, string newTargetTag, string newTargetTag2)
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
        targetTag = newTargetTag;
        targetTag2 = newTargetTag2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            DestructionSequence();
        }
        if (collision.tag == targetTag2)
        {
            DestructionSequence();
        }

    }

    public void DestructionSequence()
    {
        projectileSprite.SetActive(false);
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Instantiate(explosion, this.transform);
        if (p_Sys != null)
            p_Sys.Stop();
        Destroy(this.gameObject, 5f);
    }
}
