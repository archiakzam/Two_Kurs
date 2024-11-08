using System;

namespace LabRab9 {
    public class Money
    {
        private int rubles;
        private int kopeks;

        private static int objectCount = 0;

        public int Rubles
        {
            get => rubles;
            set => rubles = value >= 0 ? value : 0;
        }

        public int Kopeks
        {
            get => kopeks;
            set => kopeks = (value >= 0 && value < 100) ? value : 0;
        }

       
        public Money(int rubles, int kopeks)
        {
            Rubles = rubles + kopeks / 100;
            Kopeks = kopeks % 100;           
            objectCount++;
        }
        public Money() : this(0, 0) { }

        public void Display()
        {
            Console.WriteLine($"Money: {Rubles} rubles, {Kopeks} kopeks");
        }

        public static Money AddKopeks(Money money, int additionalKopeks)
        {
            int totalKopeks = money.Kopeks + additionalKopeks;
            int newRubles = money.Rubles + totalKopeks / 100;
            int newKopeks = totalKopeks % 100;
            return new Money(newRubles, newKopeks);
        }

        public Money AddKopeks(int additionalKopeks)
        {
            int totalKopeks = Kopeks + additionalKopeks;
            Rubles += totalKopeks / 100;
            Kopeks = totalKopeks % 100;
            return this;
        }
        public static Money operator ++(Money m)
        {
            int totalKopeks = m.Kopeks + 1;
            m.Rubles += totalKopeks / 100;
            m.Kopeks = totalKopeks % 100;
            return m;
        }

        // Перегрузка  -- (вычитает 1 копейку)
        public static Money operator --(Money m)
        {
            if (m.Kopeks > 0)
            {
                m.Kopeks -= 1;
            }
            else if (m.Rubles > 0)
            {
                m.Rubles -= 1;
                m.Kopeks = 99;
            }
            else
            {
                m.Rubles = 0;
                m.Kopeks = 0;
            }
            return m;
        }

        public static explicit operator int(Money m)
        {
            return m.Rubles;
        }

        public static implicit operator double(Money m)
        {
            return m.Kopeks / 100.0;
        }

        public static Money operator +(Money m, int value)
        {
            int totalKopeks = m.Kopeks + value;
            int newRubles = m.Rubles + totalKopeks / 100;
            int newKopeks = totalKopeks % 100;
            return new Money(newRubles, newKopeks);
        }

        public static Money operator +(int value, Money m)
        {
            return m + value;
        }

        public static Money operator -(Money m, int value)
        {
            int totalKopeks = m.Rubles * 100 + m.Kopeks - value;
            if (totalKopeks < 0)
            {
                return new Money(0, 0);
            }
            int newRubles = totalKopeks / 100;
            int newKopeks = totalKopeks % 100;
            return new Money(newRubles, newKopeks);
        }

        public static Money operator -(int value, Money m)
        {
            int totalKopeks = value * 100 - (m.Rubles * 100 + m.Kopeks);
            if (totalKopeks < 0)
            {
                return new Money(0, 0);
            }
            int newRubles = totalKopeks / 100;
            int newKopeks = totalKopeks % 100;
            return new Money(newRubles, newKopeks);
        }
        public static int ObjectCount => objectCount;
    }
}