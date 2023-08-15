using System;
using System.Threading.Tasks;
using Managers;

public class TimeManager : Manager
{
    public static TimeManager Instance;

    public float Time => PlayTime;
    public float PlayTime;
    
    public float Minute;
    public int Hour;
    public int Day;
    public event OnDayDelegate OnDay;
    public delegate void OnDayDelegate(int day);
    
    public event OnHourDelegate OnHour;
    public delegate void OnHourDelegate(int hour);

    public static bool Paused;
    public bool CanBePaused;

    public override void OnStartServer()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Update()
    {
        if (!isServer) return;
        if (Paused) return;
        Minute += UnityEngine.Time.deltaTime;
        PlayTime += UnityEngine.Time.deltaTime;

        if (Minute >= 120) // 120 - seconds - each in-game hour has 120 real life seconds
        {
            Minute = 0;
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

    public override Task<object> SaveData() => Task.FromResult<object>(new SavableData()
    {
        PlayTime = PlayTime,
        Time = Minute,
        Hour = Hour,
        Day = Day,
    });

    public override Task LoadData(object data)
    {
        var saveData = (SavableData)data;

        PlayTime = saveData.PlayTime;
        Minute = saveData.Time;
        Hour = saveData.Hour;
        Day = saveData.Day;
        return Task.CompletedTask;
    }
    
    [Serializable]
    private struct SavableData
    {
        public float PlayTime;
        public float Time;
        public int Hour;
        public int Day;
    }
}
