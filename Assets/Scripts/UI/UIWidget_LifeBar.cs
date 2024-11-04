using UnityEngine;

public class UIWidget_LifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Unit _trackedUnit;

    private void Start()
    {
        _progressBar.SetValue(_trackedUnit.Health.HealthPointsNormalized);

        _trackedUnit.Health.OnHealthValueChanged += OnHealthValueChanged;
    }

    private void OnDisable()
    {
        _trackedUnit.Health.OnHealthValueChanged -= OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float newValueNormalized)
    {
        _progressBar.SetValue(newValueNormalized);
    }
}
