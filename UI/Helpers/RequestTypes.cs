using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.UI.Helpers
{
    public static class RequestTypes
    {

        public static string Error { get { return "Something went wrong!"; }}
        public static string LoginError { get { return "Cant login now. Try later"; } }
    }
}
