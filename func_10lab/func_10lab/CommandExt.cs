using func_10lab.Commands;
using func_10lab.Domain.Events;
namespace func_10lab
{
    public static class CommandExt
    {
        public static DebitedTransfer ToEvent(this MakeTransfer cmd)
           => new DebitedTransfer
           {
               Beneficiary = cmd.Beneficiary,
               Bic = cmd.Bic,
               DebitedAmount = cmd.Amount,
               EntityId = cmd.DebitedAccountId,
               Iban = cmd.Iban,
               Reference = cmd.Reference,
               Timestamp = cmd.Timestamp
           };
    }
}
