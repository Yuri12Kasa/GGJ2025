using UnityEngine;

[CreateAssetMenu(fileName = "SentencesPool", menuName = "Scriptable Objects/SentencesPool")]
public class SentencesPool : ScriptableObject
{
    public int players;
    public Sentence[] sentences;
}
