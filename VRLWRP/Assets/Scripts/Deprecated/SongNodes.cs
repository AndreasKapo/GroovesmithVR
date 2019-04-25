using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SongNode
{
    public float minute;
    public float second;
    public float centisecond;

    public float floatTimestamp;

}
[CreateAssetMenu(fileName = "Song", menuName = "SongNodeList")]
public class SongNodes : ScriptableObject {
    public List<SongNode> nodes;
	
}
