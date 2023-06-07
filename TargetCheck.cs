using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetCheck : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] int totalTargets;
    [SerializeField] int destroyedTargets;

    [Header("UI")]
    [SerializeField] TMP_Text percentageText;
    [SerializeField] Slider percentageSlider;
    [SerializeField] TMP_Text percentageLastText;

    [SerializeField] gameManagement _gameManagement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            TargetDestroyed();
            Destroy(other.gameObject,2);
        }
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject,2);
        }
    }
    void TargetDestroyed()
    {
        destroyedTargets++;
        float percentage = (float)destroyedTargets / totalTargets * 100f;
        UpdatePercentageUI(percentage);
        UpdatePercentageBar(percentage);

        percentageLastText.text="%" + Mathf.RoundToInt(percentage);
    }

    void UpdatePercentageUI(float percentage)
    {
        percentageText.text = "%" + Mathf.RoundToInt(percentage);
    }
    public void UpdatePercentageBar(float percentage)
    {
        percentageSlider.value = percentage;
    }

    private void Update()
    {
        if (destroyedTargets == totalTargets)
        {
            _gameManagement.gameOverCheck();
        }
    }
}
