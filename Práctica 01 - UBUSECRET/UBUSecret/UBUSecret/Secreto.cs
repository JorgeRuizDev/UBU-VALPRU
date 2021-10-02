using System;
using System.Collections.Generic;
using System.Text;

namespace UBUSecret
{
    class Secreto
    {
        /*
         De cada secreto creado se crea una instancia distinta para cada receptor.
        */

        int idSecreto;
        string remitente;
        string emailRemitente;
        string receptor;
        string mensaje;
        private static int IDSEQ = 0;



        private Secreto(string remitente, string emailRemitente, string receptor, string mensaje)
        {
            this.idSecreto  =  IDSEQ++;
            this.emailRemitente = emailRemitente;
            this.receptor  =  receptor;
            this.mensaje = mensaje;
        }

        public int IdSecreto { get => idSecreto; set => idSecreto = value; }
        public string Remitente { get => remitente; set => remitente = value; }
        public string Receptor { get => receptor; set => receptor = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public string EmailRemitente { get => emailRemitente; set => emailRemitente = value; }

        public List<Secreto> EnviarSecreto(string remitente, string emailRemitente, List<string> receptores, string mensaje)
        {
            List<Secreto> secretos = new List<Secreto>();


            foreach (string receptor in receptores)
            {
                secretos.Add(new Secreto(remitente, emailRemitente, receptor, mensaje));
            }

            return secretos;
        }

        


    }
}
