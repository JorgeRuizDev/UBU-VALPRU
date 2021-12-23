using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UBUSecret
{
    [JsonObject(MemberSerialization.OptIn)]
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
        private List<string> emails;
        private string remitenteMail;


        public Secreto(Usuario remitente, LinkedList<Usuario> receptores, string mensaje, string titulo, string alias)
        {
            this.idSecreto  =  IDSEQ++;
            this.mensaje = mensaje;
            this.remitente = remitente;
            this.Receptores = receptores;
            this.Alias = alias;
            this.Titulo = titulo;
            this.DiaHora = DateTime.UtcNow;

            if (receptores != null) { 
                var emails = new List<String>();
                foreach (var u in receptores)
                {
                    emails.Add(u.Email);
                }
                this.emails = emails;

                remitenteMail = remitente.Email;
            }
        }

        [JsonProperty]
        public int IdSecreto { get => idSecreto; set => idSecreto = value; }
        
        public Usuario Remitente { get => remitente; set => remitente = value; }
        [JsonProperty]
        public string Mensaje { get => mensaje; set => mensaje = value; }

        public LinkedList<Usuario> Receptores { get => receptores; set => receptores = value; }
        [JsonProperty]
        public string Titulo { get => titulo; set => titulo = value; }
        [JsonProperty]
        public string Alias { get => alias; set => alias = value; }
        [JsonProperty]
        public DateTime DiaHora { get => diaHora; set => diaHora = value; }
        [JsonProperty]
        public List<string> Emails { get => emails; set => emails = value; }
        [JsonProperty]
        public string RemitenteMail { get => remitenteMail; set => remitenteMail = value; }

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

        public override string ToString()
        {
            return String.Format("Id: {0}", this.IdSecreto);
        }
    }
}
