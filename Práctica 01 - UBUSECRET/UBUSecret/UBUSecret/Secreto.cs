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
        Usuario remitente;
        string emailRemitente;
        string alias;
        string mensaje;
        string titulo;
        LinkedList<Usuario> receptores;
        private static int IDSEQ = 0;



        public Secreto(Usuario remitente, string emailRemitente, LinkedList<Usuario> receptores, string mensaje, string titulo, string alias)
        {
            this.idSecreto  =  IDSEQ++;
            this.emailRemitente = emailRemitente;
            this.mensaje = mensaje;
            this.remitente = remitente;
            this.Receptores = receptores;
            this.Alias = alias;
            this.Titulo = titulo;

        }

        public int IdSecreto { get => idSecreto; set => idSecreto = value; }
        public Usuario Remitente { get => remitente; set => remitente = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        public string EmailRemitente { get => emailRemitente; set => emailRemitente = value; }
        public LinkedList<Usuario> Receptores { get => receptores; set => receptores = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Alias { get => alias; set => alias = value; }

        public Boolean TieneAcceso(Usuario usuario) {
            return receptores.Find(usuario) != null;

        }
    }
}
