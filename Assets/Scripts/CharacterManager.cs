using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour {
    public Pins pinsDB;

    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        UpdateCharacter();
    }

    void UpdateCharacter() {
        Pin current = pinsDB.GetPin(selection);
        sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
        nameLabel.SetText(current.name);
    }

    public void Next() {
        int numberPins = pinsDB.GetCount();
        if (selection < numberPins - 1) {
            selection++;
        }
        else {
            selection = 0;
        }
        UpdateCharacter();
    }

    public void Previous() {
        if (selection > 0) {
            selection--;
        }
        else {
            selection = pinsDB.GetCount() - 1;
        }
        UpdateCharacter();
    }
}
