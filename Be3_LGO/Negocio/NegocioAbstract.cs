using System;
using System.Reflection;
using Be3_LGO.lib.Persistencia.dbDB;

namespace Be3_LGO.lib.Negocio
{
    public abstract class NegocioAbstract
    {
        internal Contexto Contexto { get; private set; }

        public NegocioAbstract()
        {
            Contexto = new Contexto();
        }

        public NegocioAbstract(Contexto contexto)
        {
            Contexto = contexto == null ? new Contexto() : contexto;
        }
    }
}
