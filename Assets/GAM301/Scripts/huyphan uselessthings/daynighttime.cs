using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayLengthInMinutes = 5f;

    [Header("References")]
    public Light sunLight;

    [Header("Date Settings")]
    public int startDay = 1;
    public int startMonth = 1;

    [Header("Season Settings")]
    public Image seasonIcon; // Icon để hiển thị mùa
    public Sprite springIcon, summerIcon, autumnIcon, winterIcon;

    [Header("Skybox Settings")]
    public Material springSkybox;
    public Material summerSkybox;
    public Material autumnSkybox;
    public Material winterSkybox;
    public Material nightSkybox;
    public Material rainSkybox; // Skybox khi trời mưa

    [Header("Daytime Color Settings")]
    public Gradient dayCycleColor; // Gradient duy nhất cho cả ngày

    [Header("Light Settings")]
    [Range(0f, 2f)] public float minIntensity = 0.25f;
    [Range(0f, 2f)] public float maxIntensity = 1f;
    public float intensityChangeSpeed = 0.5f;

    [Header("Weather Settings")]
    public ParticleSystem rainParticle;
    public ParticleSystem snowParticle;
    public bool isRaining = false; // Trạng thái mưa (public để điều chỉnh thủ công)
    public bool isSnowing = false; // Trạng thái tuyết (public để điều chỉnh thủ công)
    public float rainChance = 0.2f; // Tỷ lệ mưa (0.2 = 20%)
    public float snowChance = 0.2f; // Tỷ lệ tuyết vào mùa đông
    public float minRainDurationInMinutes = 5f; // Độ dài mưa tối thiểu
    public float maxRainDurationInMinutes = 15f; // Độ dài mưa tối đa
    private float rainTimer;
    private float weatherCheckIntervalInHours = 6f;
    private float nextWeatherCheckTime;

    // Các biến đã có của bạn giữ nguyên
    private float timeMultiplier;
    private float currentTimeOfDay;
    private int day;
    private int month;
    private float targetIntensity;

    void Start()
    {
        day = startDay;
        month = startMonth;
        timeMultiplier = 1440f / (dayLengthInMinutes * 60f);
        currentTimeOfDay = 6f; // Bắt đầu lúc 6 giờ sáng
        targetIntensity = maxIntensity;
        nextWeatherCheckTime = weatherCheckIntervalInHours; // Đặt thời gian kiểm tra thời tiết đầu tiên

        UpdateSeason();
    }

    void Update()
    {
        UpdateTimeOfDay();
        UpdateSunPosition();
        UpdateLightIntensity();
        UpdateDate();
        UpdateSeason();
        UpdateSkyColor();
        UpdateWeather();
    }

    private void UpdateTimeOfDay()
    {
        currentTimeOfDay += Time.deltaTime * timeMultiplier / 60f;
        if (currentTimeOfDay >= 24f)
        {
            currentTimeOfDay -= 24f;
            day++;
        }

        if (currentTimeOfDay >= nextWeatherCheckTime)
        {
            CheckWeather();
            nextWeatherCheckTime += weatherCheckIntervalInHours;
        }
    }

    private void UpdateSunPosition()
    {
        if (sunLight != null)
        {
            float sunAngle = (currentTimeOfDay / 24f) * 360f - 90f;
            sunLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        }
    }

    private void UpdateLightIntensity()
    {
        if (sunLight == null) return;

        sunLight.intensity = Mathf.MoveTowards(
            sunLight.intensity,
            targetIntensity,
            intensityChangeSpeed * Time.deltaTime
        );

        if (Mathf.Approximately(sunLight.intensity, targetIntensity))
        {
            targetIntensity = targetIntensity == maxIntensity ? minIntensity : maxIntensity;
        }
    }

    private void UpdateDate()
    {
        int[] daysInMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (day > daysInMonths[month - 1])
        {
            day = 1;
            month++;
            if (month > 12)
            {
                month = 1;
            }
        }
    }

    private void UpdateSeason()
    {
        if (seasonIcon == null) return;

        if (month >= 3 && month <= 5)
        {
            seasonIcon.sprite = springIcon;
        }
        else if (month >= 6 && month <= 8)
        {
            seasonIcon.sprite = summerIcon;
        }
        else if (month >= 9 && month <= 11)
        {
            seasonIcon.sprite = autumnIcon;
        }
        else
        {
            seasonIcon.sprite = winterIcon;
        }
    }

    private void UpdateSkyColor()
    {
        if (RenderSettings.skybox == null) return;

        float timePercentage = currentTimeOfDay / 24f;
        sunLight.color = dayCycleColor.Evaluate(timePercentage);

        if (currentTimeOfDay >= 5f && currentTimeOfDay < 19.20f) // Ban ngày
        {
            if (isRaining && rainSkybox != null)
            {
                RenderSettings.skybox = rainSkybox;
            }
            else if (isSnowing && rainSkybox != null)
            {
                RenderSettings.skybox = rainSkybox; // Skybox mưa cho tuyết
            }
            else
            {
                if (month >= 3 && month <= 5)
                    RenderSettings.skybox = springSkybox;
                else if (month >= 6 && month <= 8)
                    RenderSettings.skybox = summerSkybox;
                else if (month >= 9 && month <= 11)
                    RenderSettings.skybox = autumnSkybox;
                else
                    RenderSettings.skybox = winterSkybox;
            }
        }
        else // Ban đêm
        {
            RenderSettings.skybox = isRaining || isSnowing ? rainSkybox : nightSkybox;
        }
    }

    private void CheckWeather()
    {
        float chance = Random.value;
        if (month >= 12 || month <= 2) // Mùa đông
        {
            isSnowing = chance < snowChance;
            isRaining = false; // Không có mưa vào mùa đông
        }
        else
        {
            isRaining = chance < rainChance;
            isSnowing = false;
        }

        if (isRaining || isSnowing)
        {
            float rainDuration = Random.Range(minRainDurationInMinutes, maxRainDurationInMinutes);
            rainTimer = rainDuration * 60f;
        }
    }

    public void UpdateWeather()
    {
        if (rainTimer > 0f)
        {
            rainTimer -= Time.deltaTime;
        }
        else
        {
            isRaining = false;
            isSnowing = false;
        }

        // Điều chỉnh trạng thái mưa
        if (isRaining && rainParticle != null)
        {
            if (!rainParticle.isPlaying) rainParticle.Play();
        }
        else if (!isRaining && rainParticle != null)
        {
            if (rainParticle.isPlaying) rainParticle.Stop();
        }

        // Điều chỉnh trạng thái tuyết
        if (isSnowing && snowParticle != null)
        {
            if (!snowParticle.isPlaying) snowParticle.Play();
        }
        else if (!isSnowing && snowParticle != null)
        {
            if (snowParticle.isPlaying) snowParticle.Stop();
        }
    }

    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(currentTimeOfDay);
        int minutes = Mathf.FloorToInt((currentTimeOfDay - hours) * 60);
        return $"{hours:D2}:{minutes:D2}";
    }

    public string GetFormattedDate()
    {
        return $"{day:D2}/{month:D2}";
    }
}
