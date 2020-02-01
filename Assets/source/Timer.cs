using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    class Timer
    {
        float timeLeft = 0.0f;

        public Timer(float time)
        {
            timeLeft = time;
        }

        public void SetCountdown(float time)
        {
            timeLeft = time;
        }

        public void Countdown(float dt)
        {
            timeLeft -= dt;
        }

        public bool IsTimeUp()
        {
            return timeLeft <= 0.0f;
        }
    }
}
