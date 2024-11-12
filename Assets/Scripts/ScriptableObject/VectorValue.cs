using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject,ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
