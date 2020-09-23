using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Factories
{
    interface Ifactory
    {
        public Iproduct convert(Iproduct product);
    }
}
