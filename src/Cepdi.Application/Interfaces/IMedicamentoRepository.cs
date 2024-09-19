﻿using ENTITIES = Cepdi.Domain.Entities;

namespace Cepdi.Application.Interfaces
{
    public interface IMedicamentoRepository
    {
        Task<IEnumerable<ENTITIES.Medicamentos>> GetMedicamentoAsync();
        Task<IEnumerable<ENTITIES.Medicamentos>> GetMedicamentoByIdAsync(int id);
        Task<bool> AddMedicamentoAsync(ENTITIES.Medicamentos data);
        Task<bool> UpdateMedicamentoAsync(ENTITIES.Medicamentos data);
    }
}
