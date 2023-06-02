using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class CatchError
    {
        public string id { set; get; }
        public string ViolationError { set; get; }

        public DateTime? ImmediatelyPunished { set; get; }

        public ulong? PenaltyDays { set; get; }
        public ulong? FinedAmount { set; get; }
        public string Coddingregion { set; get; }
        public string personcatcherror { set; get; }
        public CatchError() { }
        public CatchError(string id,string ViolationError,DateTime imediately,ulong days,ulong amount,string codding,string personcatcherror)
        {
            this.id = id;
            this.ViolationError = ViolationError;
            this.ImmediatelyPunished = imediately;
            this.PenaltyDays = days;
            this.FinedAmount = amount;
            this.Coddingregion = codding;
            this.personcatcherror = personcatcherror;
        }
    }
}
