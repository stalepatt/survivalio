using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Util
{
    public class Timer
    {
        public ElapsedTime Elapsed { get; private set; }

        private int _maxTime;

        private CancellationTokenSource _cancellationToken;

        public event Action OnTimeChanged;

        public bool IsTimerOn { get; private set; }

        public void Init()
        {
            Elapsed = new ElapsedTime();
        }

        public void Start(bool isContinue = false)
        {
            Time.timeScale = 1f;

            if (Elapsed.Time != 0 && !isContinue)
            {
                Elapsed.Clear();
            }

            if (_cancellationToken != null)
            {
                _cancellationToken.Dispose();
            }
            _cancellationToken = new CancellationTokenSource();

            CountElapsedTimeTask().Forget();
        }

        public void StartTimerInRange(int minTime, int maxTime)
        {
            Elapsed.Min = minTime / 60;
            Elapsed.Sec = minTime / 60;
            _maxTime = maxTime;

            Start(isContinue: true);
        }

        public void Stop()
        {
            if (IsTimerOn)
            {
                Time.timeScale = 0;
                _cancellationToken.Cancel();
                IsTimerOn = false;
            }
        }

        public void Continue()
        {
            Start(isContinue: true);
        }

        public void Reset()
        {
            if (IsTimerOn)
            {
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
                IsTimerOn = false;
            }

            Elapsed.Clear();
        }

        public void Clear() => this.Reset();

        private async UniTaskVoid CountElapsedTimeTask()
        {
            if (Elapsed == null)
            {
                Elapsed = new ElapsedTime();
            }
            IsTimerOn = true;

            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false, PlayerLoopTiming.Update, _cancellationToken.Token);
                Elapsed.Sec += 1;

                if (Elapsed.Sec >= 60)
                {
                    Elapsed.Sec = 0;
                    Elapsed.Min += 1;
                }

                OnTimeChanged.Invoke();

                if (Elapsed.Time >= _maxTime)
                {
                    break;
                }
            }
            IsTimerOn = false;
        }

        public Timer(int maxTimeSec = int.MaxValue)
        {
            _maxTime = maxTimeSec;
            Init();
        }

    }

    public class ElapsedTime
    {
        public int Time
        {
            get
            {
                return Min * 60 + Sec;
            }
        }
        public int Min;
        public int Sec;

        public void Clear()
        {
            Min = 0;
            Sec = 0;
        }
    }
}