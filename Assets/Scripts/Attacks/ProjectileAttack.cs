using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileAttack : DamageComponent
{
    [SerializeField] private float m_shrinkSpeed = 1.5f;
    [SerializeField] private float m_projectileOffset = 0.5f;
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private GameObject[] m_onLineEndPrefab;
    private float m_lineWidth;
    public override void Initialize()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        if (m_lineRenderer == null)
        {
            m_lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        m_lineRenderer.startWidth = 0.5f;

        var materials = new List<Material> { GameManager.Instance.RangeAttackMaterial };
        m_lineRenderer.SetMaterials(materials);

        m_lineWidth = m_lineRenderer.startWidth;
        m_lineRenderer.sortingLayerName = "VFX";
    }
    public override void PerformVisualAttack(float damage, Vector2 target, Vector2 center)
    {
        // TODO: Get RANDOM point in target to instantiate damage bubles
        Vector3[] positions = { center, GetRandomPositionFromCenter(target, m_projectileOffset) };
        m_lineRenderer.SetPositions(positions);
        m_lineRenderer.startWidth = m_lineWidth;

        StartCoroutine(ShrinkAnimation());
    }

    private Vector2 GetRandomPositionFromCenter(Vector2 center, float offset)
    {
        Vector2 result = center + Random.insideUnitCircle * offset;
        return result;
    }

    private IEnumerator ShrinkAnimation()
    {
        float width = m_lineRenderer.startWidth;

        while (width > 0.0f)
        {
            width -= Time.deltaTime * m_shrinkSpeed;
            m_lineRenderer.startWidth = width;

            yield return new WaitForEndOfFrame();
        }
    }
}
