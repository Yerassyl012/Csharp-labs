using LaYumba.Functional;
using static LaYumba.Functional.F;
using System;

public class Interview_Example_Option
{
    public static Func<Candidate, bool> IsEligible;
    public static Func<Candidate, Option<Candidate>> Interview;

    public static Option<Candidate> FirstRound(Candidate c)
       => Some(c)
          .Where(IsEligible)
          .Bind(Interview);
}

public class Interview_Example_Either
{
    public static Func<Candidate, bool> IsEligible;
    public static Func<Candidate, Either<Rejection, Candidate>> Interview;

    public static Either<Rejection, Candidate> CheckEligibility(Candidate c)
    {
        if (IsEligible(c)) return c;
        return new Rejection("Not eligible");
    }

    public static Either<Rejection, Candidate> FirstRound(Candidate c)
       => Right(c)
          .Bind(CheckEligibility)
          .Bind(Interview);
}

public class Candidate 
{
    public string name { get; set; }
    public Candidate(string name)
    {
        this.name = name;
    }
}
public class Rejection
{
    private string reason;

    public Rejection(string reason)
    {
        this.reason = reason;
    }
}