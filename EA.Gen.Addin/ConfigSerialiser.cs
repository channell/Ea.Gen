using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace EA.Gen.Addin
{
    /// <summary>
    /// Class for serialising configuraiton customisation JSON strings
    /// </summary>
    public static class ConfigSerialiser
    {
        private static DataContractJsonSerializer _serialiser
            = new DataContractJsonSerializer(typeof(Model.Customisation));

        /// <summary>
        /// Convert the object graph to a string that can be stored in a database
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ToString (Model.Customisation c)
        {
            var m = new MemoryStream();
            _serialiser.WriteObject(m, c);
            var reader = new StreamReader(m);
            m.Position = 0;
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Converrt a JSON string into a customisation object graph
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Model.Customisation ToGraph (string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            var m = new MemoryStream(b);
            return (Model.Customisation)_serialiser.ReadObject(m);
        }
    }
}
