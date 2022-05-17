using System;
using UnityEngine;

public interface IPlayerUpgrade
{
    event Action<int> OnLevelUp;

    string Id { get; }
    int Level { get; }
    int NextPrice { get; }
    string CurrentStats { get; }
    string NextImprovement { get; }

    void Setup(int level);
}
