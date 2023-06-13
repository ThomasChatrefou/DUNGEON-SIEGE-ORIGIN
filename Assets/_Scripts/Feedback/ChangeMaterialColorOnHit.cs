using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class ChangeMaterialColorOnHit : MonoBehaviour
{
    [SerializeField] private Material _takeDamageMaterial;
    [SerializeField] private float _duration = 0.1f;

    private CharacterHealth _characterHealth;
    private Material _baseMaterial;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _baseMaterial = _meshRenderer.material;
    }

    private void OnEnable()
    {
        _characterHealth.OnHitEvent += DamageFeedbackCall;
    }

    private void OnDisable()
    {
        _characterHealth.OnHitEvent -= DamageFeedbackCall;
    }

    private void DamageFeedbackCall()
    {
        StartCoroutine(CODamageFeedback());
    }

    private IEnumerator CODamageFeedback()
    {
        _meshRenderer.material = _takeDamageMaterial;
        yield return new WaitForSeconds(_duration);
        _meshRenderer.material = _baseMaterial;
    }
}
