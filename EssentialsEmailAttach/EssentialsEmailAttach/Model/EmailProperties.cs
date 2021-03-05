using System;
using System.Collections.Generic;
using System.Text;

namespace EssentialsEmailAttach.Model
{
    public class EmailProperties
    {
        /// <summary>
        /// Atributos para enviar el Email
        /// </summary>
        public string subject { get; set; }
        public string body { get; set; }
        public List<string> to { get; set; }
        //public string to { get; set; }
        public string attachment { get; set; }
    }
}
