using System;
using System.Collections.Generic;
using System.Text;

namespace UBUSecret
{
    public class Secreto
    {

        private int idSecreto;
        private Usuario remitente;
        private string alias;
        private string mensaje;
        private string titulo;
        private DateTime diaHora;
        private LinkedList<Usuario> receptores;
        private static int IDSEQ = 0;



        public Secreto(Usuario remitente, LinkedList<Usuario> receptores, string mensaje, string titulo, string alias)
        {
            this.idSecreto  =  IDSEQ++;
            this.mensaje = mensaje;
            this.remitente = remitente;
            this.Receptores = receptores;
            this.Alias = alias;
            this.Titulo = titulo;
            this.DiaHora = DateTime.UtcNow;

        }

        public int IdSecreto { get => idSecreto; set => idSecreto = value; }
        public Usuario Remitente { get => remitente; set => remitente = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }

        

        public LinkedList<Usuario> Receptores { get => receptores; set => receptores = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Alias { get => alias; set => alias = value; }
        public DateTime DiaHora { get => diaHora; set => diaHora = value; }

        public bool TieneAcceso(Usuario usuario) {
            if (usuario != null)
            {
                return receptores.Find(usuario) != null;
            }
            else { return false; }
        }

        public void DarAcceso(Usuario usuario) {
            if (usuario != null) {
                receptores.AddLast(usuario);
            }

        }

        public bool QuitarAcceso(Usuario usuario) {
            if (usuario != null)
            {
                return receptores.Remove(usuario);
            }
            else { return false; }
        }


    }
}
