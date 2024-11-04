using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _imgFiller;
    [SerializeField] private TextMeshProUGUI _textValue;

    public void SetValue(float valueNormalized)
    {
        _imgFiller.fillAmount = valueNormalized;

        int valueInPercent = Mathf.RoundToInt(valueNormalized * 100);
        _textValue.text = $"{valueInPercent}%";
    }
}
