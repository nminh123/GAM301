//using UnityEngine;

//public class WeatherController : MonoBehaviour
//{
//    [Header("Weather Settings")]
//    [Tooltip("Tỷ lệ xuất hiện mưa (từ 0 đến 1)")]
//    public float rainChance = 0.1f;
//    [Tooltip("Tỷ lệ xuất hiện tuyết (từ 0 đến 1)")]
//    public float snowChance = 0.05f;
//    [Tooltip("Thời gian tối thiểu của cơn mưa/tuyết (phút)")]
//    public float minDuration = 1f;
//    [Tooltip("Thời gian tối đa của cơn mưa/tuyết (phút)")]
//    public float maxDuration = 10f;

//    [Header("Particle Systems")]
//    public ParticleSystem rainParticle; // Particle System cho mưa
//    public ParticleSystem snowParticle; // Particle System cho tuyết

//    private DayNightCycle dayNightCycle;
//    private float weatherTimer;
//    private bool isWeatherActive = false;

//    void Start()
//    {
//        Debug.Log("WeatherController đã khởi động!"); 

//        dayNightCycle = FindObjectOfType<DayNightCycle>();
//        ResetWeatherTimer();
//    }

//    void Update()
//    {
//        weatherTimer -= Time.deltaTime / 60f; // Chuyển đổi sang phút

//        if (weatherTimer <= 0)
//        {
//            if (isWeatherActive)
//            {
//                EndWeather(); // Kết thúc cơn mưa/tuyết hiện tại
//            }
//            else
//            {
//                TryStartWeather(); // Thử bắt đầu một cơn mưa/tuyết mới
//            }
//            ResetWeatherTimer();
//        }
//    }

//    private void ResetWeatherTimer()
//    {
//        if (isWeatherActive)
//        {
//            weatherTimer = Random.Range(minDuration, maxDuration); // Thời gian kéo dài thời tiết
//        }
//        else
//        {
//            weatherTimer = Random.Range(5f, 15f); // Thời gian chờ đến lần kiểm tra tiếp theo
//        }
//    }

//    private void TryStartWeather()
//    {
//        Debug.Log("Đang thử bắt đầu thời tiết...");
//        bool rain = Random.value < rainChance;
//        bool snow = !rain && Random.value < snowChance;

//        if (rain || snow)
//        {
//            Debug.Log(rain ? "Bắt đầu mưa" : "Bắt đầu tuyết");
//            isWeatherActive = true;
//            dayNightCycle.SetWeather(rain, snow);

//            // Bật Particle System tương ứng
//            if (rain && rainParticle != null)
//            {
//                rainParticle.Play();
//            }
//            else if (snow && snowParticle != null)
//            {
//                snowParticle.Play();
//            }
//        }
//        else
//        {
//            Debug.Log("Không có thời tiết xảy ra lần này.");
//        }
//    }

//    private void EndWeather()
//    {
//        Debug.Log("Kết thúc thời tiết hiện tại...");
//        isWeatherActive = false;
//        dayNightCycle.SetWeather(false, false);

//        // Tắt Particle System
//        if (rainParticle != null && rainParticle.isPlaying)
//        {
//            rainParticle.Stop();
//        }
//        if (snowParticle != null && snowParticle.isPlaying)
//        {
//            snowParticle.Stop();
//        }
//    }

//}
