using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ChangeLightShape : MonoBehaviour
{
    public float lightBaseRadius = 50.0f;
    public float noise = 10.0f;
    public float noiseSpeed = 1.0f;
    public float offset = 0.0f;

    new Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int nSegments = light.shapePath.Length;
        float angleInc = (Mathf.PI * 2.0f) / nSegments;

        for (int i = 0; i < nSegments; i++)
        {
            float   angle = i * angleInc;
            float   r = lightBaseRadius + noise * Mathf.PerlinNoise(angle, offset);
            Vector3 p = new Vector3(r * Mathf.Cos(angle), r * Mathf.Sin(angle), 0.0f);

            light.shapePath[i] = p;
        }

        offset += Time.deltaTime * noiseSpeed;
    }
}
