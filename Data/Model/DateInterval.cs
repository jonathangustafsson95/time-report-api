using Data.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    class DateInterval: Iproduct
    {
        DateTime startDay;
        DateTime EndDay;
        List<DateTime> Days;
        public DateInterval(DateTime start, DateTime End)
        {
            this.startDay = start;
            this.EndDay = End;
        }
        //aasda

    }
}
