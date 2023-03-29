using System;
using UnityEngine;

public class RendererFadeOut : MonoBehaviour
{
    public event EventHandler Faded;

    [SerializeField] private float fadeTime = 1f; // tiempo que tarda en desaparecer el objeto
    float alpha = 1f; // valor alpha actual
    private Material material; // material del objeto

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer= GetComponent<Renderer>();
        material = renderer.material;

        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;

        // actualizar el valor alpha del material utilizando Lerp
        alpha = Mathf.Lerp(alpha, 0f, Time.deltaTime / fadeTime);
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

        // si el objeto ya no es visible, destruirlo
        if (alpha <= 0.1f)
        {
            if(Faded != null)
            {
                Faded.Invoke(this, new EventArgs());
            }
        }
    }

    public void StartFadeOut()
    {
        alpha = material.color.a;

        started = true;
    }
}
