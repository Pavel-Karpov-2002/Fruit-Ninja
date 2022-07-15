using UnityEngine;
using System;

[Serializable]
public class ComboSettings
{
    [SerializeField] private float timeDisappearanceComboText;

    [SerializeField] private int maxCombo;

    [SerializeField] private float timeFadeComboText;

    public int MaxCombo => maxCombo;

    public float TimeDisappearanceComboText => timeDisappearanceComboText;

    public float TimeFadeComboText => timeFadeComboText;
}
