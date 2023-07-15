using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPromptController : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnInteractShow(string prompt)
    {
        _textMeshPro.SetText(prompt);
    }

    public void SetShow(bool show)
    {
        _canvas.gameObject.SetActive(show); 
    }
}
