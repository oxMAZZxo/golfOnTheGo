using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    /// <summary>
    /// The ID of this Player
    /// </summary>
    [property: SerializeField]public int ID { get; }
    /// <summary>
    /// The Name of this player
    /// </summary>
    [property: SerializeField]public string Name { get; }
    /// <summary>
    /// The colour used for this Players sprite
    /// </summary>
    [property: SerializeField]public Color Colour {get;}
    /// <summary>
    /// This players Score
    /// </summary>
    public int Score {get; set;}
    /// <summary>
    /// The number of tries in this current run. This should reset everytime the level changes.
    /// </summary>
    public int Tries {get; set;}
    /// <summary>
    /// The PlayerController assigned to this player.
    /// </summary>
    public PlayerController Controller {get; set;}

    public PlayerData(int id, string name, Color color, int score = 0, int attempts = 0)
    {
        ID = id;
        Name = name;
        Colour = color;
        Score = score;
        Tries = attempts;
    }
}