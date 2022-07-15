using System;
using UnityEngine;

[Serializable]
public class HealthSettings
{
    [SerializeField] private int startHealth;
    [SerializeField] private int maxHealth;

    public int StartHealth => startHealth;
    public int MaxHealth => maxHealth;
}
