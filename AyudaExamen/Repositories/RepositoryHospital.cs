using AyudaExamen.Data;
using AyudaExamen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AyudaExamen.Repositories
{
    #region PROCEDURES
    //create procedure SP_SALAS_HOSPITAL
    //(@posicion int, @hospitalid int, @registros int out)
    //as
    // select @registros = count(SALA_COD) from SALA where HOSPITAL_COD = @hospitalid;

    //    select SALA_COD, NOMBRE, NUM_CAMA, HOSPITAL_COD
    //    from(
    //        select
    //            ROW_NUMBER() over (order by SALA_COD) as POSICION, 
    //            SALA_COD, 
    //            NOMBRE, 
    //            NUM_CAMA, 
    //            HOSPITAL_COD
    //        from SALA
    //        where HOSPITAL_COD = @hospitalid
    //    ) as QUERY
    //    where QUERY.POSICION >= @posicion
    //      and QUERY.POSICION< (@posicion + 2);
    //go

    //create procedure SP_SALA_HOSPITAL(@posicion int, @hospitalid int, @numregistros int OUT)
    //as
    //    select @numregistros = Count(SALA_COD) from SALA where HOSPITAL_COD = @hospitalid
    //    select SALA_COD, NOMBRE, NUM_CAMA, HOSPITAL_COD from(
    //    select cast(ROW_NUMBER() over (order by SALA_COD) as int)
    //        as POSICION, SALA_COD, NOMBRE, NUM_CAMA, HOSPITAL_COD
    //        from SALA
    //        where HOSPITAL_COD=@hospitalid) QUERY
    //        where(QUERY.POSICION = @posicion)
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

        public async Task<Hospital> FindHospitalAsync(int idhospital)
        {
            return await this.context.Hospitales.FindAsync(idhospital);
        }

        public async Task<int> GetNumeroSalasHospitalAsync(int idhospital)
        {
            return await this.context.Salas.Where(s => s.HospitalId == idhospital).CountAsync();
        }

        public async Task<List<Sala>> GetSalasHospitalAsync(int posicion, int idhospital)
        {
            string sql = "SP_SALAS_HOSPITAL @posicion, @idhospital, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdHospital = new SqlParameter("@idhospital", idhospital);
            SqlParameter pamRegistros = new SqlParameter("@registros", 0);
            pamRegistros.Direction = ParameterDirection.Output;
            pamRegistros.DbType = DbType.Int32;

            List<Sala> salas = await this.context.Salas.FromSqlRaw(sql, pamPosicion, pamIdHospital, pamRegistros).ToListAsync();

            return salas;
        }

        public async Task<Sala> GetSalaByPosicionAsync(int posicion, int idhospital)
        {
            string sql = "SP_SALA_HOSPITAL @posicion, @idhospital, @numregistros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdHospital = new SqlParameter("@idhospital", idhospital);
            SqlParameter pamNumRegistros = new SqlParameter("@numregistros", 0);
            pamNumRegistros.Direction = ParameterDirection.Output;
            pamNumRegistros.DbType = DbType.Int32;

            var consulta = await this.context.Salas.FromSqlRaw(sql, pamPosicion, pamIdHospital, pamNumRegistros).ToListAsync();

            Sala sala = consulta.FirstOrDefault();

            return sala;
        }
    }
}