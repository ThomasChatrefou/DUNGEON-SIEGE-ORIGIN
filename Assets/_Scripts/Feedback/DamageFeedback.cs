using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProtoHealth), typeof(MeshRenderer))]
public class DamageFeedback : MonoBehaviour
{
    public Material TakeDamageMaterial;

    private ProtoHealth _protoHealth;
    private Material _baseMaterial;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _protoHealth = GetComponent<ProtoHealth>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _baseMaterial = _meshRenderer.material;
    }

    private void OnEnable()
    {
        _protoHealth._onHitEvent += DamageFeedbackCall;
    }
    private void OnDisable()
    {
        _protoHealth._onHitEvent -= DamageFeedbackCall;
    }

    public void DamageFeedbackCall()
    {
        StartCoroutine(CODamageFeedback());
}

private IEnumerator CODamageFeedback()
{
    _meshRenderer.material = TakeDamageMaterial;
    yield return new WaitForSeconds(0.1f);
    _meshRenderer.material = _baseMaterial;
}
}
