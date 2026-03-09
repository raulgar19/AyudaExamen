using AyudaExamen.Data;
using AyudaExamen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AyudaExamen.Repositories
{
    #region VIEWS AND PROCEDURES
    //create view V_SALAS_HOSPITAL
    //as
    //  select cast(ROW_NUMBER() over (order by SALA_COD) as int) 
    //  as POSICION, SALA_COD, NOMBRE, NUM_CAMA, HOSPITAL_COD
    //  from SALA
    //go

    //create procedure SP_SALAS_HOSPITAL
    //(@posicion int, @hospitalid int, @registros int out)
    //as
    //    select @registros = count(SALA_COD) from SALA where HOSPITAL_COD = @hospitalid;
    //    select SALA_COD, NOMBRE, NUM_CAMA, HOSPITAL_COD
    //    from(
    //        select POSICION, SALA_COD, NOMBRE, NUM_CAMA, HOSPITAL_COD
    //        from V_SALAS_HOSPITAL
    //        where HOSPITAL_COD = @hospitalid
    //    ) as QUERY
    //    where QUERY.POSICION >= @posicion
    //    and QUERY.POSICION< (@posicion + 3);
    //go

    #endregion

    public class RepositoryHospital
    {
        private HospitalContext context;

        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Hospital>> GetHospitales()
        {
            return await this.context.Hospitales.ToListAsync();
        }

        public async Task<Hospital> FindHospital(int idhospital)
        {
            return await this.context.Hospitales.FindAsync(idhospital);
        }

        public async Task<SalasModel> GetSalasHospital(int posicion, int idhospital)
        {
            string sql = "SP_SALAS_HOSPITAL @posicion, @idhospital, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdHospital = new SqlParameter("@idhospital", idhospital);
            SqlParameter pamRegistros = new SqlParameter("@registros", 0);
            pamRegistros.Direction = ParameterDirection.Output;
            pamRegistros.DbType = DbType.Int32;

            List<Sala> salas = await this.context.Salas.FromSqlRaw(sql, pamPosicion, pamIdHospital, pamRegistros).ToListAsync();
            int registros = (int)pamRegistros.Value;

            return new SalasModel
            {
                Salas = salas,
                Registros = registros
            };
        }
    }
}