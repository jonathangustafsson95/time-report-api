using Data.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    class DateInterval: Iproduct
    {
        DateTime start;
        DateTime End;

        public DateInterval(DateTime start, DateTime End)
        {
            this.start = start;
            this.End = End;
        }
        //aasda

    }
}
