using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Commands
{
    public class CreateActivityRejected : IRejectedCommand
    {
        public Guid Id { get; }
        public string Code { get; }
        public string Reason { get; }

        protected CreateActivityRejected()
        {
        }

        public CreateActivityRejected(Guid id, string code, string reason)
        {
            Id = id;
            Code = code;
            Reason = reason;
        }
    }
}
