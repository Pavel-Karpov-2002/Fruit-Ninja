using UnityEngine;
using System;

[Serializable]
public class ComboSettings
{
    [SerializeField][Min(0)] private float timeDisappearanceComboText;

    [SerializeField][Min(0)] private int maxCombo;

    [SerializeField][Min(0)] private float timeFadeComboText;

    public int MaxCombo => maxCombo;

    public float TimeDisappearanceComboText => timeDisappearanceComboText;

    public float TimeFadeComboText => timeFadeComboText;
}
