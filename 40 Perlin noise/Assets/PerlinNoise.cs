using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {
    public int height = 1024;
    public int width = 1024;
    public int scale = 100;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        Color color;
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                color = GetColor(x, y);
                texture.SetPixel(x, y, color);
            }
        texture.Apply();
        return texture;
    }

    Color GetColor(float x, float y)
    {
        x = x / width * scale;
        y = y / height * scale;

        float sample = Mathf.PerlinNoise(x, y);
        return new Color(sample, sample, sample);
    }
}
