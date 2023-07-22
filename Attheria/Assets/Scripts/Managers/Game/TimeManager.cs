using System;
using Mirror;
using SaveSystem.SaveLoad;

[SaveFile("Game")]
public class TimeManager : NetworkBehaviour, ISaveable
{
    public static TimeManager Instance;
    
    public float Time;
    public int Hour;
    public int Day;
    public event OnDayDelegate OnDay;
    public delegate void OnDayDelegate(int day);
    
    public event OnHourDelegate OnHour;
    public delegate void OnHourDelegate(int hour);

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Update()
    {
        Time += UnityEngine.Time.deltaTime;

        if (Time >= 120) // 120 - seconds - each in-game hour has 120 real life seconds
        {
            Time = 0;
            Hour++;
            OnHour?.Invoke(Hour);
        }

        if (Hour == 24) // 24 hours / day
        {
            Hour = 0;
            Day++;
            OnDay?.Invoke(Day);
        }
    }

    public object SaveData() => new SavableData()
    {
        Time = Time,
        Hour = Hour,
        Day = Day,
    };

    public void LoadData(object data)
    {
        var saveData = (SavableData)data;

        Time = saveData.Time;
        Hour = saveData.Hour;
        Day = saveData.Day;
    }
    
    [Serializable]
    private struct SavableData
    {
        public float Time;
        public int Hour;
        public int Day;
    }
}
