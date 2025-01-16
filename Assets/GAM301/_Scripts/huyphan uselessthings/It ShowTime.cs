using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public TextMeshProUGUI timeText; // Hiển thị thời gian
    public TextMeshProUGUI dateText; // Hiển thị ngày tháng

    void Update()
    {
        if (dayNightCycle != null)
        {
            // Cập nhật thông tin thời gian và ngày tháng
            timeText.text = dayNightCycle.GetFormattedTime();
            dateText.text = dayNightCycle.GetFormattedDate();
        }
    }
}
