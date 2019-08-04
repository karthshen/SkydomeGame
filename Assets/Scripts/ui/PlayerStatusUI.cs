using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    public Image characterIcon;
    public Image healthBar;
    public Image manaBar;

    private AActor player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AActor>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercentage = player.CurrentHealth / player.ActorData.MaxHealth;
        float manaPercentage = player.CurrentEnergy / player.ActorData.MaxEnergy;

        healthBar.GetComponent<RectTransform>().localScale = new Vector3(healthPercentage, 1, 1);
        manaBar.GetComponent<RectTransform>().localScale = new Vector3(manaPercentage, 1, 1);
    }
}
