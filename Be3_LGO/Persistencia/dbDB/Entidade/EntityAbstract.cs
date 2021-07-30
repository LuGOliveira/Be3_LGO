using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be3_LGO.lib.Persistencia.dbDB.Entidade
{
    public abstract class EntityAbstract
    {
        public Enum.AcaoPersistencia AcaoPersistencia { get; set; }

        public Guid IdObjeto { get; set; }

        public abstract List<Inconsistencia> Validar();

        public EntityAbstract()
        {
            IdObjeto = Guid.NewGuid();
        }
    }
}