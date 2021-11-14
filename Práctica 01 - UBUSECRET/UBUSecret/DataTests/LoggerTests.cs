using Microsoft.VisualStudio.TestTools.UnitTesting;
using UBUSecret;
using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Interfaces;

namespace UBUSecret.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod()]
        public void LogTest()
        {
            //Obtengo una instancia
            ICapaDatos bd = DBPruebas.ObtenerInstacia();

            //Reseteamos los Log
            bd.ResetLogs();

            //Comprobamos que esta vacia
            Assert.AreEqual(bd.LeerLogs().Count, 0);

            //Añadimos el nuevo log
            Logger.Log("Prueba",Level.ERROR);

            //Comprobamos que contiene el log
            Assert.AreEqual(bd.LeerLogs().Count, 1);

            //Reseteamos los Log
            bd.ResetLogs();

            //Comprobamos que esta vacia
            Assert.AreEqual(bd.LeerLogs().Count, 0);


        }
    }
}