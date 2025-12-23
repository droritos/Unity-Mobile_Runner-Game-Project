

/*
using DG.Tweening;
using TMPro;
using UnityEngine;



public class Indicator : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI valueText;
    [SerializeField] private float startYOffset = 30f;

    [Header("Movement")]
    [SerializeField] private float moveDuration = 0.6f;
    [SerializeField] private float moveDistance = 60f;
    [SerializeField] private float horizontalSpread = 10f;
    [SerializeField] private float randomOffset = 2.5f;

    [Header("Animation")]
    [SerializeField] private float popInDuration = 0.2f;
    [SerializeField] private float fadeOutDuration = 0.4f;
    [SerializeField] private float popInScale = 1.2f;

    [Header("Colors")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color healingColor = Color.green;
    [SerializeField] private Color criticalColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color goldColor = Color.yellow;

    private Vector3 startPosition;

    public void SetText(int value, ValueTypes type)
    {
        valueText.SetText(value.ToString());
        
        switch (type)
        {
            case ValueTypes.Damage:
                valueText.color = damageColor;
                break;
            case ValueTypes.Healing:
                valueText.color = healingColor;
                break;
            case ValueTypes.Critical:
                valueText.color = criticalColor;
                break;
            case ValueTypes.Gold:
                valueText.color = goldColor;
                break;
        }
    }

    void OnEnable()
    {
        // Kill any existing tweens to prevent interference
        transform.DOKill();
        valueText.DOKill();

        // Reset all properties
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        valueText.alpha = 1f;
        
        // Set position first, then store it
        transform.position = new Vector3(transform.position.x, transform.position.y + startYOffset,0);
        startPosition = transform.position;

        // Pop in
        transform.DOScale(Vector3.one * popInScale, popInDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                transform.DOScale(Vector3.one, popInDuration * 0.5f);
            });

        // Movement
        float randomX = Random.Range(-horizontalSpread, horizontalSpread);
        Vector3 endPosition = startPosition + new Vector3(randomX, moveDistance, 0);
        
        transform.DOMove(endPosition, moveDuration)
            .SetEase(Ease.OutSine);

        // Fade out
        valueText.DOFade(0, fadeOutDuration)
            .SetEase(Ease.InSine)
            .SetDelay(moveDuration * 0.5f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
*/


using DG.Tweening;
using TMPro;
using UnityEngine;
public enum ValueTypes
{
    Damage,
    Healing,
    Critical,
    Gold
}
public class Indicator : MonoBehaviour
{
    [Header("References")]
    public TextMeshPro valueText; 

    [Header("Movement")]
    [SerializeField] private float moveDuration = 0.8f;
    [SerializeField] private float moveDistance = 2.0f;
    [SerializeField] private float horizontalSpread = 0.5f;

    [Header("Animation")]
    [SerializeField] private float popInDuration = 0.2f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private float popInScale = 1.5f;

    [Header("Colors")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color healingColor = Color.green;
    [SerializeField] private Color criticalColor = new Color(1f, 0.65f, 0f);
    [SerializeField] private Color goldColor = Color.yellow;

    public void SetText(int value, ValueTypes type)
    {
        // 1. Setup Visuals
        valueText.text = value.ToString();
        
        switch (type)
        {
            case ValueTypes.Damage:
                valueText.color = damageColor;
                valueText.fontSize = 6;
                break;
            case ValueTypes.Healing:
                valueText.color = healingColor;
                valueText.fontSize = 6;
                break;
            case ValueTypes.Critical:
                valueText.color = criticalColor;
                valueText.fontSize = 8;
                valueText.text += "!";
                break;
            case ValueTypes.Gold:
                valueText.color = goldColor;
                valueText.fontSize = 6;
                valueText.text = "+" + value;
                break;
        }

        // 2. Animate
        AnimateFloating();
    }

    private void AnimateFloating()
    {
        transform.DOKill();
        valueText.DOKill();

        transform.localScale = Vector3.one;
        valueText.alpha = 1f;

        // Pop In
        transform.DOScale(Vector3.one * popInScale, popInDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => transform.DOScale(Vector3.one, popInDuration * 0.5f));

        // Move
        float randomX = Random.Range(-horizontalSpread, horizontalSpread);
        Vector3 endPosition = transform.position + new Vector3(randomX, moveDistance, 0);

        transform.DOMove(endPosition, moveDuration).SetEase(Ease.OutCirc);

        // Fade & DISABLE (Don't Destroy)
        valueText.DOFade(0, fadeOutDuration)
            .SetEase(Ease.InSine)
            .SetDelay(moveDuration - fadeOutDuration)
            .OnComplete(() => gameObject.SetActive(false)); 
    }
}