using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text.RegularExpressions;


namespace func1_lab
{
    public interface IValidator<T>
    {
        bool IsValid(T t);
    }
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }

    // "real" implementation
    public class DefaultDateTimeService : IDateTimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }

    // testable class depends on interface
    public class DateNotPastValidator_Testable : IValidator<MakeTransfer>
    {
        private readonly IDateTimeService clock;

        public DateNotPastValidator_Testable(IDateTimeService clock)
        {
            this.clock = clock;
        }

        public bool IsValid(MakeTransfer request)
           => clock.UtcNow.Date <= request.Date.Date;
    }

    // can be tested using fakes
    public class DateNotPastValidatorTest
    {
        static DateTime presentDate = new DateTime(2016, 12, 12);

        // "fake" implementation
        private class FakeDateTimeService : IDateTimeService
        {
            public DateTime UtcNow => presentDate;
        }

        public void WhenTransferDateIsPast_ThenValidatorFails()
        {
            var sut = new DateNotPastValidator_Testable(new FakeDateTimeService());
            var transfer = new MakeTransfer { Date = presentDate.AddDays(-1) };
            Assert.AreEqual(false, sut.IsValid(transfer));
        }

        public bool WhenTransferDateIsPast_ThenValidationFails(int offset)
        {
            var sut = new DateNotPastValidator_Testable(new FakeDateTimeService());
            var transferDate = presentDate.AddDays(offset);
            var transfer = new MakeTransfer { Date = transferDate };

            return sut.IsValid(transfer);
        }
    }

    public class DateNotPastValidator : IValidator<MakeTransfer>
    {
        DateTime Today { get; }

        public DateNotPastValidator(DateTime today)
        {
            this.Today = today;
        }

        public bool IsValid(MakeTransfer cmd)
           => Today <= cmd.Date.Date;
    }
    public abstract class Command
    {
        public DateTime Timestamp { get; set; }

        public T WithTimestamp<T>(DateTime timestamp)
           where T : Command
        {
            T result = (T)MemberwiseClone();
            result.Timestamp = timestamp;
            return result;
        }
    }

    public class MakeTransfer : Command
    {
        public Guid DebitedAccountId { get; set; }

        public string Beneficiary { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
    }

    public sealed class BicFormatValidator : IValidator<MakeTransfer>
    {
        static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$");

        public bool IsValid(MakeTransfer t)
           => regex.IsMatch(t.Bic);
    }

    public sealed class BicExistsValidator_Skeleton : IValidator<MakeTransfer>
    {
        readonly IEnumerable<string> validCodes;

        public bool IsValid(MakeTransfer cmd)
           => validCodes.Contains(cmd.Bic);
    }

    public sealed class BicExistsValidator : IValidator<MakeTransfer>
    {
        readonly Func<IEnumerable<string>> getValidCodes;

        public BicExistsValidator(Func<IEnumerable<string>> getValidCodes)
        {
            this.getValidCodes = getValidCodes;
        }

        public bool IsValid(MakeTransfer cmd)
           => getValidCodes().Contains(cmd.Bic);
    }

    public class BicExistsValidatorTest
    {
        static string[] validCodes = { "ABCDEFGJ123" };

        public bool WhenBicNotFound_ThenValidationFails(string bic)
           => new BicExistsValidator(() => validCodes)
              .IsValid(new MakeTransfer { Bic = bic });
    }

}
