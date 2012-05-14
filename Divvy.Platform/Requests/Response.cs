using System;
using System.Collections.Generic;
using System.Text;

namespace Divvy.Platform.Requests
{
    public enum ResponseStatus
    {
        Okay = 200,
        Error = 500,
        Killed = 600
    }

    public struct Response
    {
        ResponseStatus Status;
    }
}
