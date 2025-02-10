using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DashIconBehavior : MonoBehaviour {
    TextMeshProUGUI tmp;
    Image overlay;
    float cooldown = 0.0f;
    float cooldownRate = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) {
            if (images[i].tag == "Overlay") {
                overlay = images[i];
            }
        }
        overlay.fillAmount = 0.0f;

        cooldownRate = PinBehavior.cooldownRate;
    }

    // Update is called once per frame
    void Update() {
        cooldown = PinBehavior.cooldown;

        string message = "";
        if (cooldown > 0) {
            float fill = cooldown / cooldownRate;
            message = string.Format("{0:0.0}", cooldown);
            overlay.fillAmount = fill;
        }
        tmp.SetText(message);
    }
}
