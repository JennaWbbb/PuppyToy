using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppyToy.Models.Enum {
    public enum LockActionType {

        Invalid,
        SessionStart,
        SessionEnd,
        RequirementLink,
        VoteAdded,
        VoteRemoved,
        PilloryEntered,
        PilloryAdded,
        SelfAdded,
        ApiAdded,
        ApiRemoved,
        WheelOfFortune

    }
}
