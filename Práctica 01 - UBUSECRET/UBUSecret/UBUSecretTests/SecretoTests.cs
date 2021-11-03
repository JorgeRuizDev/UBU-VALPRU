using Microsoft.VisualStudio.TestTools.UnitTesting;
using UBUSecret;
using System;
using System.Collections.Generic;
using System.Text;

namespace UBUSecret.Tests
{
    [TestClass()]
    public class SecretoTests
    {

        private Usuario remitente;

        private Usuario receptorA;
        private Usuario receptorB;
        private Usuario receptorC;
        private Usuario receptorD;

        private LinkedList<Usuario> receptores;
        private Secreto secreto;

        [TestInitialize()]
        public void Inicializacion()
        {
            receptores = new LinkedList<Usuario>();
            
            this.remitente = new Usuario("Pedro", "Piqueras", "pedro@ubu.es", "631 882 831", "1234");

            receptorA = new Usuario("Fernando", "Pozo", "fer@ubu.es", "631 452 831", "1222");
            receptores.AddLast(receptorA);

            receptorB = new Usuario("Mariano", "Espada", "marian@ubu.es", "631 765 831", "1232");
            receptores.AddLast(receptorB);

            receptorC = new Usuario("Pepe", "Lobato", "pepe@ubu.es", "631 098 831", "1216");
            receptores.AddLast(receptorC);

            receptorD = new Usuario("NoEstoy", "Lobato", "pepe@ubu.es", "631 098 831", "1216");

            secreto = new Secreto(remitente, receptores, "Hola este es el mensaje", "HOLA", "nosoyyo");

        }


        [TestMethod()]
        public void SecretoTest()
        {
            new Secreto(remitente, receptores, "Hola este es el mensaje", "HOLA", "nosoyyo");
        }

        [TestMethod()]
        public void TieneAccesoTest()
        {
            //Comprobamos que si tiene acceso un usuario añadido a la lista
            Assert.IsTrue(secreto.TieneAcceso(receptorC));

            //Comprobamos que no tiene acceso un usuario que no ha sido añadido
            Assert.IsFalse(secreto.TieneAcceso(receptorD));
        }

        [TestMethod()]
        public void DarAccesoTest()
        {
            //Comprobamos que el usuario no tiene acceso
            Assert.IsFalse(secreto.TieneAcceso(receptorD));

            //Le damos acceso
            secreto.DarAcceso(receptorD);

            //Comprobamos que ahora si tiene acceso
            Assert.IsTrue(secreto.TieneAcceso(receptorD));

        }

        [TestMethod()]
        public void QuitarAccesoTest()
        {
            //Damos acceso al usuario
            DarAccesoTest();

            //Quitamos acceso al usuario
            secreto.QuitarAcceso(receptorD);

            //Comprobamos que el usuario no tiene acceso
            Assert.IsFalse(secreto.TieneAcceso(receptorD));
            

           
        }
    }
}