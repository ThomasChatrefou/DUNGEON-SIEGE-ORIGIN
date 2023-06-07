using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChangeColorEffect : MonoBehaviour, IAbilityVisualEffect
{
    [SerializeField] private float _playDuration = 0.4f;
    [SerializeField] private Color _playColor = Color.white;
    [SerializeField] private Color _previewColor;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = _previewColor;
    }

    private IEnumerator ChangeSpriteColorForPlayDuration()
    {
        _renderer.color = _playColor;
        yield return new WaitForSeconds(_playDuration);
        _renderer.color = _previewColor;
    }

    public void Play()
    {
        StartCoroutine(ChangeSpriteColorForPlayDuration());
    }
}