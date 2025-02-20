using UnityEngine;

[CreateAssetMenu(fileName = "Pins", menuName = "Scriptable Objects/Pins", order = 1)]
public class Pins : ScriptableObject {
    public Pin[] pins;

    public int GetCount() {
        return pins.Length;
    }

    public Pin GetPin(int index) {
        return pins[index];
    }
}
