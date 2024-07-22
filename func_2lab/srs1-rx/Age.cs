using System;
using LaYumba.Functional;

namespace srs1_rx
{
    using static F;

    public struct Age
    {
        private int Value { get; }
        private Age(int value)
        {
            if (!IsValid(value))
                throw new ArgumentException($"{value} is not a valid age");

            Value = value;
        }

        private static bool IsValid(int age)
           => 0 <= age && age < 120;

        public static Option<Age> Of(int age)
           => IsValid(age) ? Some(new Age(age)) : None;

        public static bool operator <(Age l, Age r) => l.Value < r.Value;
        public static bool operator >(Age l, Age r) => l.Value > r.Value;

        public static bool operator <(Age l, int r) => l < new Age(r);
        public static bool operator >(Age l, int r) => l > new Age(r);

        public override string ToString() => Value.ToString();
    }
    enum Risk { Low, Medium, High }

    public class Bind_Example
    {
        internal void RunExamples()
        {
            CalculateRiskProfile_Dynamic("hello"); //ошибка времени выполнения: оператор '<' нельзя применить к операндам типа 'string' и 'int'
        }

        Risk CalculateRiskProfile_Dynamic(dynamic age)
           => (age < 60) ? Risk.Low : Risk.Medium;


        Risk CalculateRiskProfile_Int(int age)
           => (age < 60) ? Risk.Low : Risk.Medium;

        Risk CalculateRiskProfile_Throws(int age)
        {
            if (age < 0 || 120 <= age)
                throw new ArgumentException($"{age} is not a valid age");

            return (age < 60) ? Risk.Low : Risk.Medium;
        }

        Risk CalculateRiskProfile(Age age)
           => (age < 60) ? Risk.Low : Risk.Medium;

        Risk CalculateRiskProfile(Age age, bool smoker)
           => (age < 60)
              ? Risk.Low
              : (smoker) ? Risk.High : Risk.Medium;
    }
}
