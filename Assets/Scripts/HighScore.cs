using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private const float FONTWIDTH = 26f;
    private const float OFFSET = 30f;

    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private RectTransform _rectTransform;
    private float _width;
    private uint _digits;
    private uint _counter;

    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
        _textMeshPro.text = GameManager.Instance.HighScore.ToString();
        _width = FONTWIDTH + OFFSET;
        _digits = 0;
        _counter = 10;
    }

    void FixedUpdate()
    {
        bool isDigitIncreasing = (GameManager.Instance.HighScore / _counter) > 0;
        if (isDigitIncreasing) {
            _digits++;
            _counter *= 10;
            _width = OFFSET + FONTWIDTH * (_digits + 1);
            _rectTransform.sizeDelta = new Vector2(_width, _rectTransform.sizeDelta.y);
        }
        _textMeshPro.text = GameManager.Instance.HighScore.ToString();
    }
}
