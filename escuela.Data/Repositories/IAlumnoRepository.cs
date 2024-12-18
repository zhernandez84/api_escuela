using NetCoreAPIMySQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Data.Repositories
{
    public interface IAlumnoRepository
    {
        Task<SearchAlumnoResponse> GetAllAlumno();
        Task<SearchAlumnoResponse> GetAlumno(string busca);
        Task<RegisterAlumnoResponse> InsertAlumno(Alumno alumno);
        Task<RegisterAlumnoResponse> UpdateAlumno(Alumno alumno);
        Task<RegisterAlumnoResponse> DeleteAlumno(Alumno alumno);
        Task<RegisterCalificacionResponse> InsertCalificacion(Calificacion calificacion);
        Task<SearchCalificacionResponse> BuscarCalificacion(SearchCalificacionRequest searchCalificacionRequest);
        Task<UserLoginResponse> UserLogin(UserLoginRequest request);

    }
}
