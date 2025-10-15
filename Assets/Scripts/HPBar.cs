using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class HPBar : MonoBehaviour{
    [Header("UI設定")]
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerController player;
    private int maxHP;

    [SerializeField] private TMP_Text valueHPText;
    [SerializeField] private TMP_Text maxHPText;

    void Start(){
        if (player == null)
            player = FindAnyObjectByType<PlayerController>();

        // スライダーの初期設定
        slider.maxValue = player.maxHP;
        slider.value = player.hp;

        maxHPText.text = player.maxHP.ToString();
        valueHPText.text = player.hp.ToString();
    }

    void Update(){
        slider.value = player.hp;
        valueHPText.text = player.hp.ToString();
    }
}
