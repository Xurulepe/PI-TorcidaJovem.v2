using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Musical Note Data", menuName = "Scriptable Objects/Musical Note Data")]
public class MusicalNoteDataSO : ScriptableObject
{
    public int scoreValue;
    public float timingWindow;
    public string accuracyInfo;
    public VertexGradient noteGradient;
    public float targetScale = 1.2f;
    public float pulseDuration = 0.08f;
}
