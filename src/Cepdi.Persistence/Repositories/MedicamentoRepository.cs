using Cepdi.Application.Interfaces;
using Cepdi.Domain.Entities;
using Cepdi.Persistence.Context;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Persistence.Repositories
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicamentoRepository(ApplicationDbContext context) 
        {
            this._context = context;
        }

        public async Task<bool> AddMedicamentoAsync(Medicamentos data)
        {
            var response = -1;
            string qry = "spActualizaMedicamentos";
            var parameters = new DynamicParameters();
            parameters.Add("nombre", data.NOMBRE);
            parameters.Add("concentracion", data.CONCENTRACION);
            parameters.Add("precio", data.PRECIO);
            parameters.Add("acciones", data.ACCIONES);
            parameters.Add("presentacion", data.PRESENTACION);
            parameters.Add("habilitado", data.HABILITADO);

            using (var conn = this._context.CreateConnection)
            {
                response = await conn.ExecuteScalarAsync<int>(qry, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return response > 0;
        }

        public async Task<IEnumerable<Medicamentos>> GetMedicamentosAsync()
        {
            IEnumerable<Medicamentos> response = null;
            string qry = "spAObtenerMedicamentos";


            using (var conn = this._context.CreateConnection)
            {
                response = await conn.QueryAsync<Medicamentos>(qry, commandType: System.Data.CommandType.StoredProcedure);
            }

            return response;
        }

        public async Task<Medicamentos> GetMedicamentoByIdAsync(int id)
        {
            IEnumerable<Medicamentos> response = null;
            string qry = "spAObtenerMedicamentos";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var conn = this._context.CreateConnection)
            {
                response = await conn.QueryAsync<Medicamentos>(qry, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return response.FirstOrDefault();
        }

        public async Task<bool> UpdateMedicamentoAsync(Medicamentos data)
        {
            var response = -1;
            string qry = "spActualizaMedicamentos";
            var parameters = new DynamicParameters();
            parameters.Add("idMedicamento", data.IDMEDICAMENTO);
            parameters.Add("nombre", data.NOMBRE);
            parameters.Add("concentracion", data.CONCENTRACION);
            parameters.Add("precio", data.PRECIO);
            parameters.Add("acciones", data.ACCIONES);
            parameters.Add("presentacion", data.PRESENTACION);
            parameters.Add("habilitado", data.HABILITADO);


            using (var conn = this._context.CreateConnection)
            {
                 response = await conn.ExecuteScalarAsync<int>(qry, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return response > 0;
        }

        public async Task<bool> DeleteMedicamnetoAsync(int id)
        {
            var medicamento = await this.GetMedicamentoByIdAsync(id);

            if (medicamento is null) { return false; }

            medicamento.HABILITADO = false;

            return await this.UpdateMedicamentoAsync(medicamento);
        }
    }
}
