using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTarget : MonoBehaviour
{
    public Material enemiesDefaultMaterial;
    [SerializeField] Material silhoutteMaterial;

    Renderer enemyRenderer;

    SphereCollider sphere;
    Collider newCollider;

    private bool trigger;

    // Start is called before the first frame update
    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (!this.sphere.enabled)
        {
            if (this.newCollider.gameObject.tag == "Enemy" && this.newCollider.gameObject == null)
            {
                var enemyBody = this.newCollider.transform.Find("Body_low");

                this.enemyRenderer = enemyBody.gameObject.GetComponent<Renderer>();
                this.enemyRenderer.sharedMaterial = enemiesDefaultMaterial;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.newCollider = other;
        this.trigger = true;

        if (other.gameObject.tag == "Enemy")
        {
            var enemyBody = other.transform.Find("Body_low");
            this.enemyRenderer = enemyBody.gameObject.GetComponent<Renderer>();
            this.enemyRenderer.sharedMaterial = silhoutteMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        newCollider = other;
        trigger = false;

        if (other.gameObject.tag == "Enemy")
        {
            var enemyBody = other.transform.Find("Body_low");
            this.enemyRenderer = enemyBody.gameObject.GetComponent<Renderer>();
            this.enemyRenderer.sharedMaterial = enemiesDefaultMaterial;
        }
    }
}
