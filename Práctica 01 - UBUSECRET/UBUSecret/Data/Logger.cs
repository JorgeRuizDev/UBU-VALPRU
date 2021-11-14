using System;
using System.Collections.Generic;
using System.Text;
using UBUSecret;
using Interfaces;
using Data;

namespace UBUSecret
{
    public class Logger
    {
        

        private Logger() {
            
        }

        public static void Log(String entry, Level level) {
            ICapaDatos bd = DBPruebas.ObtenerInstacia();
            bd.AñadirLog(new Log(entry, level));
        }
    }
}
