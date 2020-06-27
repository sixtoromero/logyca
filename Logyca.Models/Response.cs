using System;
using System.Collections.Generic;
using System.Text;

namespace Logyca.Models
{
    public class Response<T>
    {
        /// <summary>
        /// Aquí se recibe el resultado de la operación pedida
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Nos indica si fue exitoso o no el proceso
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Mensaje a mostrar en alguna operación realizada sea para error o otro tipo de acción
        /// </summary>
        public string Message { get; set; }
    }
}
