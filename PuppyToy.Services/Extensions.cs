using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppyToy.Services {
    public static class Extensions {

        public static bool In<T>( this T t, IEnumerable<T> values ) {
            return values.Contains( t );
        }

    }
}