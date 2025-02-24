using TMPro;
using UnityEngine;

public class ImStarving : MonoBehaviour
{
    [SerializeField] DayNightCycle time;
    [SerializeField] TextMeshProUGUI text;
    public int starvingProcess = 100;

    void Start()
    {
        if (PlayerPrefs.HasKey("Starving"))
        {
            starvingProcess = PlayerPrefs.GetInt("Starving");
            Debug.Log("Starving process loaded: " + starvingProcess);
            text.text = starvingProcess.ToString();
        }
        else
        {
            Debug.Log("Không tìm thấy key 'Starving' trong PlayerPrefs. Gán giá trị mặc định.");
        }
    }

    void Update()
    {
        if (time == null) return;
        if (time.isHungry)
        {
            starvingProcess -= 2;
            text.text = starvingProcess.ToString();
            print("Starving: " + starvingProcess);
            PlayerPrefs.SetInt("Starving", starvingProcess);
            PlayerPrefs.Save();
        }
    }
}