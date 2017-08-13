using System;
using UnityEngine;

/// <summary>
/// Contains the player informations that will be sent to the server as a json.
/// </summary>
[Serializable]
public class Player : MonoBehaviour {

    public string questCode;
    public string name;
    public int points;

}
