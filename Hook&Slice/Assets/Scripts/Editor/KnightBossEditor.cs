using UnityEditor;

[CustomEditor(typeof(KnightBoss))]
public class KnightBossEditor : Editor
{
    public override void OnInspectorGUI()
    {
        KnightBoss knight = (KnightBoss)target;
    }
}
