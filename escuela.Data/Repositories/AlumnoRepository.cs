using Google.Protobuf;
using MySql.Data.MySqlClient;
using NetCoreAPIMySQL.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Data.Repositories
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private MySQLConfiguration _connectionString;
        public AlumnoRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        public async Task<UserLoginResponse> UserLogin(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse();
            response.nCodigo = 1;
            response.sMensaje = "Successful";

            var db = dbConection();
            try
            {
                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_select_usuario(?,?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_sUsuario", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@p_sPassword", request.Password);

                    using (MySqlDataReader dataReader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.data = new UserInformation();
                            await dataReader.ReadAsync();
                            response.sMensaje = "Datos encontrados";
                            response.nCodigo = 1;
                            response.data.user = dataReader["sUsuario"] != DBNull.Value ? dataReader["sUsuario"].ToString() : string.Empty;
                        }
                        else
                        {
                            response.sMensaje = "Datos no encontrados";
                            response.nCodigo = 0;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }
        
        public async Task<SearchAlumnoResponse> GetAllAlumno()
        {
            SearchAlumnoResponse response = new SearchAlumnoResponse();

            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_get_alumnos()", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;                   

                    using (MySqlDataReader dataReader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.alumnoRespose = new List<AlumnoRespose>();
                            while (await dataReader.ReadAsync())
                            {
                                AlumnoRespose getData = new AlumnoRespose();                                
                                getData.nIdAlumno = dataReader["nIdAlumno"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdAlumno"]) : 0;
                                getData.nEstatus = dataReader["nEstatus"] != DBNull.Value ? Convert.ToInt32(dataReader["nEstatus"]) : 0;
                                getData.sEstatus = dataReader["sEstatus"] != DBNull.Value ? Convert.ToString(dataReader["sEstatus"]) : null;
                                getData.sIdAlumno = dataReader["sIdAlumno"] != DBNull.Value ? Convert.ToString(dataReader["sIdAlumno"]) : null;
                                getData.sNombre = dataReader["sNombre"] != DBNull.Value ? Convert.ToString(dataReader["sNombre"]) : null;
                                getData.sPaterno = dataReader["sPaterno"] != DBNull.Value ? Convert.ToString(dataReader["sPaterno"]) : null;
                                getData.sMaterno = dataReader["sMaterno"] != DBNull.Value ? Convert.ToString(dataReader["sMaterno"]) : null;
                                //getData.dFecNacimiento = dataReader["dFecNacimiento"] != DBNull.Value ? Convert.ToString(dataReader["dFecNacimiento"]) : null;
                                getData.dFecNacimiento = dataReader["dFecNacimiento"] != DBNull.Value ? Convert.ToDateTime(dataReader["dFecNacimiento"]) : DateTime.MinValue;
                                getData.sGenero = dataReader["sGenero"] != DBNull.Value ? Convert.ToString(dataReader["sGenero"]) : null;

                                response.alumnoRespose.Add(getData);
                            }
                            response.sMensaje = "Datos encontrados";
                            response.nCodigo = 1;
                        }
                        else
                        {
                            response.sMensaje = "Datos no encontrados";
                            response.nCodigo = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<SearchAlumnoResponse> GetAlumno(string busca)
        {
            SearchAlumnoResponse response = new SearchAlumnoResponse();

            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_search_alumno(?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_buscar", busca);

                    using (MySqlDataReader dataReader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                             
                            response.alumnoRespose = new List<AlumnoRespose>();
                            while (await dataReader.ReadAsync())
                            {
                                AlumnoRespose getData = new AlumnoRespose();
                                getData.nIdAlumno = dataReader["nIdAlumno"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdAlumno"]) : 0;
                                getData.nEstatus = dataReader["nEstatus"] != DBNull.Value ? Convert.ToInt32(dataReader["nEstatus"]) : 0;
                                getData.sEstatus = dataReader["sEstatus"] != DBNull.Value ? Convert.ToString(dataReader["sEstatus"]) : null;
                                getData.sIdAlumno = dataReader["sIdAlumno"] != DBNull.Value ? Convert.ToString(dataReader["sIdAlumno"]) : null;
                                getData.sNombre = dataReader["sNombre"] != DBNull.Value ? Convert.ToString(dataReader["sNombre"]) : null;
                                getData.sPaterno = dataReader["sPaterno"] != DBNull.Value ? Convert.ToString(dataReader["sPaterno"]) : null;
                                getData.sMaterno = dataReader["sMaterno"] != DBNull.Value ? Convert.ToString(dataReader["sMaterno"]) : null;
                               // date = dataReader["dFecNacimiento"] != DBNull.Value ? Convert.ToString(dataReader["dFecNacimiento"]) : null;
                                getData.dFecNacimiento = dataReader["dFecNacimiento"] != DBNull.Value ? Convert.ToDateTime(dataReader["dFecNacimiento"]): DateTime.MinValue;
                                getData.sGenero = dataReader["sGenero"] != DBNull.Value ? Convert.ToString(dataReader["sGenero"]) : null;
                                
                                response.alumnoRespose.Add(getData);
                            }
                            response.sMensaje = "Datos encontrados";
                            response.nCodigo = 1;
                        }
                        else
                        {
                            response.sMensaje = "Datos no encontrados";
                            response.nCodigo = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }
        public async Task<RegisterAlumnoResponse> InsertAlumno(Alumno alumno)
        {
            RegisterAlumnoResponse response = new RegisterAlumnoResponse();
            response.nCodigo = 1;
            response.sMensaje = "Registro Exitoso";
            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_insert_alumno(?,?,?,?,?,?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_sIdAlumno", alumno.sIdAlumno);
                    sqlCommand.Parameters.AddWithValue("@p_sNombre", alumno.sNombre);
                    sqlCommand.Parameters.AddWithValue("@p_sPaterno", alumno.sPaterno);
                    sqlCommand.Parameters.AddWithValue("@p_sMaterno", alumno.sMaterno);
                    sqlCommand.Parameters.AddWithValue("@p_dFecNacimiento", alumno.dFecNacimiento);
                    sqlCommand.Parameters.AddWithValue("@p_sGenero", alumno.sGenero);

                    if (await sqlCommand.ExecuteNonQueryAsync() <= 0)
                    {
                        response.nCodigo = 0;
                        response.sMensaje = "Algo salio mal en la consulta";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<RegisterAlumnoResponse> UpdateAlumno(Alumno alumno)
        {
            /* var db = dbConection();
             var sql = @"UPDATE dat_alumno SET nEstatus=@NEstatus,sIdAlumno=@SIdAlumno,`sNombre`=@sNombre,sPaterno=@SPaterno,`sMaterno`=@sMaterno,dFecNacimiento=@DFecNacimiento,sGenero=@SGenero 
                             WHERE nIdAlumno=@nIdAlumno)";
             var result = await db.ExecuteAsync(sql, new { alumno.nEstatus, alumno.sIdAlumno, alumno.sNombre, alumno.sPaterno, alumno.sMaterno, alumno.dFecNacimiento, alumno.sGenero,alumno.nIdAlumno});
             return result > 0;*/
            RegisterAlumnoResponse response = new RegisterAlumnoResponse();
            response.nCodigo = 1;
            response.sMensaje = "Actualizacion Exitosa";
            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_update_alumno(?,?,?,?,?,?,?,?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_nIdAlumno", alumno.nIdAlumno);
                    sqlCommand.Parameters.AddWithValue("@p_nEstatus", alumno.nEstatus);
                    sqlCommand.Parameters.AddWithValue("@p_sIdAlumno", alumno.sIdAlumno);
                    sqlCommand.Parameters.AddWithValue("@p_sNombre", alumno.sNombre);
                    sqlCommand.Parameters.AddWithValue("@p_sPaterno", alumno.sPaterno);
                    sqlCommand.Parameters.AddWithValue("@p_sMaterno", alumno.sMaterno);
                    sqlCommand.Parameters.AddWithValue("@p_dFecNacimiento", alumno.dFecNacimiento);
                    sqlCommand.Parameters.AddWithValue("@p_sGenero", alumno.sGenero);

                    if (await sqlCommand.ExecuteNonQueryAsync() <= 0)
                    {
                        response.nCodigo = 0;
                        response.sMensaje = "Algo salio mal en la consulta";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<RegisterAlumnoResponse> DeleteAlumno(Alumno alumno)
        {
            /* var db = dbConection();
             var sql = @"delete  from dat_alumno where nIdAlumno=@id;";
             var result= await db.ExecuteAsync(sql, new { Id = alumno.nIdAlumno });
             return result > 0;*/
            RegisterAlumnoResponse response = new RegisterAlumnoResponse();
            response.nCodigo = 1;
            response.sMensaje = "Borrado Exitoso";
            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_update_estatus_alumno(?,?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_nIdAlumno", alumno.nIdAlumno);
                    sqlCommand.Parameters.AddWithValue("@p_nEstatus", 3);

                    if (await sqlCommand.ExecuteNonQueryAsync() <= 0)
                    {
                        response.nCodigo = 0;
                        response.sMensaje = "Algo salio mal en la consulta";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<RegisterCalificacionResponse> InsertCalificacion(Calificacion calificacion)
        {
            RegisterCalificacionResponse response = new RegisterCalificacionResponse();
            response.nCodigo = 1;
            response.sMensaje = "Registro Exitoso";
            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_insert_calificacion(?,?,?,?,?,?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_nIdAlumno", calificacion.nIdAlumno);
                    sqlCommand.Parameters.AddWithValue("@p_nIdMateria", calificacion.nIdMateria);
                    sqlCommand.Parameters.AddWithValue("@p_nCalificacion", calificacion.nCalificacion);
                    sqlCommand.Parameters.AddWithValue("@p_nGrado", calificacion.nGrado);
                    sqlCommand.Parameters.AddWithValue("@p_nMes", calificacion.nMes);
                    sqlCommand.Parameters.AddWithValue("@p_nYear", calificacion.nYear);

                    if (await sqlCommand.ExecuteNonQueryAsync() <= 0)
                    {
                        response.nCodigo = 0;
                        response.sMensaje = "Algo salio mal en la consulta";
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<SearchCalificacionResponse> BuscarCalificacion(SearchCalificacionRequest searchCalificacionRequest)
        {
            SearchCalificacionResponse response = new SearchCalificacionResponse();

            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_search_calificacion(?,?,?,?)", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@p_sIdAlumno", searchCalificacionRequest.sIdAlumno);
                    sqlCommand.Parameters.AddWithValue("@p_nGrado", searchCalificacionRequest.nGrado);
                    sqlCommand.Parameters.AddWithValue("@p_nMes", searchCalificacionRequest.nMes);
                    sqlCommand.Parameters.AddWithValue("@p_nYear", searchCalificacionRequest.nYear);

                    using (MySqlDataReader dataReader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.searchCalificacion = new List<SearchCalificacion>();
                            while (await dataReader.ReadAsync())
                            {
                                SearchCalificacion getData = new SearchCalificacion();
                                getData.nIdCalificacion = dataReader["nIdCalificacion"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdCalificacion"]) : 0;
                                getData.nIdAlumno = dataReader["nIdAlumno"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdAlumno"]) : 0;
                                getData.sAlumno = dataReader["sAlumno"] != DBNull.Value ? Convert.ToString(dataReader["sAlumno"]) : null;
                                getData.nIdMateria = dataReader["nIdMateria"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdMateria"]) : 0;
                                getData.sMateria = dataReader["sMateria"] != DBNull.Value ? Convert.ToString(dataReader["sMateria"]) : null;
                                getData.nCalificacion = dataReader["nCalificacion"] != DBNull.Value ? Convert.ToDecimal(dataReader["nCalificacion"]) : 0;
                                getData.nGrado = dataReader["nGrado"] != DBNull.Value ? Convert.ToInt32(dataReader["nGrado"]) : 0;
                                getData.sGrado = dataReader["sGrado"] != DBNull.Value ? Convert.ToString(dataReader["sGrado"]) : null;
                                getData.nMes = dataReader["nMes"] != DBNull.Value ? Convert.ToInt32(dataReader["nMes"]) : 0;
                                getData.nYear = dataReader["nYear"] != DBNull.Value ? Convert.ToInt32(dataReader["nYear"]) : 0;
                                response.searchCalificacion.Add(getData);
                            }
                            response.sMensaje = "Datos encontrados";
                            response.nCodigo = 1;
                        }
                        else
                        {
                            response.sMensaje = "Datos no encontrados";
                            response.nCodigo = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<SearchMateriaResponse> GetMateria()
        {
            SearchMateriaResponse response = new SearchMateriaResponse();

            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_get_materias()", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;

                    using (MySqlDataReader dataReader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.materia = new List<Materia>();
                            while (await dataReader.ReadAsync())
                            {
                                Materia getData = new Materia();
                                getData.nIdMateria = dataReader["nIdMateria"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdMateria"]) : 0;
                                getData.sMateria = dataReader["sMateria"] != DBNull.Value ? Convert.ToString(dataReader["sMateria"]) : null;   
                                
                                response.materia.Add(getData);
                            }
                            response.sMensaje = "Datos encontrados";
                            response.nCodigo = 1;
                        }
                        else
                        {
                            response.sMensaje = "Datos no encontrados";
                            response.nCodigo = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

        public async Task<SearchGradoResponse> GetGrado()
        {
            SearchGradoResponse response = new SearchGradoResponse();

            var db = dbConection();
            try
            {

                if (db.State != System.Data.ConnectionState.Open)
                {
                    await db.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand("CALL sp_get_grados()", db))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;

                    using (MySqlDataReader dataReader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.grado = new List<Grado>();
                            while (await dataReader.ReadAsync())
                            {
                                Grado getData = new Grado();
                                getData.nIdGrado = dataReader["nIdGrado"] != DBNull.Value ? Convert.ToInt32(dataReader["nIdGrado"]) : 0;
                                getData.sGrado = dataReader["sGrado"] != DBNull.Value ? Convert.ToString(dataReader["sGrado"]) : null;

                                response.grado.Add(getData);
                            }
                            response.sMensaje = "Datos encontrados";
                            response.nCodigo = 1;
                        }
                        else
                        {
                            response.sMensaje = "Datos no encontrados";
                            response.nCodigo = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.nCodigo = 0;
                response.sMensaje = ex.Message;
            }
            finally
            {
                await db.CloseAsync();
                await db.DisposeAsync();
            }

            return response;
        }

    }
}
