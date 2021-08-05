using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Models
{
    public enum State
    {
        Active = 0, Suspended = 2, Terminated = 3
    }

    public static class enumExtenxtion
    {
        public static State ToStateEnum(this int dbstate)
        {
            switch (dbstate)
            {
                case 0:
                    return State.Active;
                case 1:
                    return State.Suspended;
                case 2:
                    return State.Terminated;
                default:
                    throw new InvalidCastException("Invalid value for State");
            }
        }
    }
}


