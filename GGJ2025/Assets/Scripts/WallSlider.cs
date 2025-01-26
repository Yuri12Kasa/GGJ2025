using System;
using UnityEngine;

public class WallSlider : MonoBehaviour
{
    [SerializeField] private float speed;
    private Material _material;
    private float _textureOffset;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _textureOffset = _material.GetTextureOffset("_BaseMap").x;
        _textureOffset += speed * Time.deltaTime;
        _material.SetTextureOffset("_BaseMap", new Vector2(_textureOffset, 0));
    }
}
