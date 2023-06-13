using System;
using UnityEngine;
using TMPro;
public class NumbersCounterUI : MonoBehaviour
{
    private TMP_Text _counterText;
    private Animator _animator;
    private void Start()
    {
        _counterText = GetComponent<TMP_Text>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CharacterInputController.NumbersCountChanged += ChangeCount;
    }

    private void OnDisable()
    {
        CharacterInputController.NumbersCountChanged -= ChangeCount;
    }

    private void ChangeCount(int amount)
    {
        _counterText.text = amount.ToString();
        _animator.SetTrigger("AddNumber");
    }
}
