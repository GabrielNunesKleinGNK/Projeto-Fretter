using FastMember;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using Microsoft.Data.SqlClient;

namespace Fretter.Repository.Util
{
    public class GenericRepository<TContext> : IGenericRepository<TContext>
        where TContext : DbContext , IUnitOfWork<TContext>
    {
        private readonly DbContext _context;
        public GenericRepository(IUnitOfWork<TContext> context)
        {
            _context = ((TContext)context);
        }

        public List<T> ExecuteStoredProcedure<T, F>(F filters, string procedureName)
        {
            procedureName = procedureName ?? $"Get{typeof(T).Name}";

            return ExecuteReader(procedureName, GetParameter(filters, procedureName)).ToList<T>();
        }

        public List<T> ExecuteStoredProcedureWithParam<T>(string procedureName, IDataParameter[] parameters)
        {
            procedureName = procedureName ?? $"Get{typeof(T).Name}";

            return ExecuteReader(procedureName, parameters).ToList<T>();
        }


        public IDataReader ExecuteReader(string commandText, IDataParameter[] parameters = null)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                _context.Database.OpenConnection();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public SqlParameter[] GetParameter(dynamic input, string procedureName)
        {
            SqlParameter[] parameters = SqlHelperParameterCache.GetSpParameterSet(_context.Database.GetDbConnection().ConnectionString, procedureName);
            Type inputType = input.GetType();

            foreach (SqlParameter parameter in parameters)
            {
                PropertyInfo property = inputType.GetProperty(parameter.ParameterName.Replace("@", ""));

                if (property == null)
                    property = inputType.GetProperty(parameter.ParameterName.Replace("@", "").ToUpper());

                if (property == null)
                    property = inputType.GetProperty(parameter.ParameterName.Replace("@", "").ToLower());

                if (property != null)
                    parameter.Value = property.GetValue(input, null);
            }

            return parameters.Where(p=> p.Value != null).ToArray();
        }

        public int ExecuteNonQuery(string commandText, IDataParameter[] parameters, CommandType commandType)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = commandType;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                _context.Database.OpenConnection();
                return command.ExecuteNonQuery();
            }
        }
        public object ExecuteScalar(string commandText, IDataParameter[] parameters, CommandType commandType)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = commandType;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                _context.Database.OpenConnection();
                return command.ExecuteScalar();
            }
        }
    }
}
