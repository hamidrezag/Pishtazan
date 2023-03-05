using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PersonnelInfoServices : BaseServices<PersonnelInfo>, IPersonnelInfoServices
    {
        private readonly IParser _parser;
        private readonly IOvertimePoliciesServices _overTimePoliciesServices;
        private readonly IConfiguration _configuration;

        public PersonnelInfoServices(AppDbContext datamodel, IParser parser, IOvertimePoliciesServices overTimePoliciesServices, IConfiguration configuration) : base(datamodel)
        {
            _parser = parser;
            _overTimePoliciesServices = overTimePoliciesServices;
            _configuration = configuration;
        }

        public List<PersonnelInfo> GetPersonnelInfoFromData(string dataType, ProcessDataAndOverTimeCalculator info)
        {
            List<PersonnelInfo> personnelInfo = _parser.Parse(dataType, info.Data);
            foreach (var item in personnelInfo)
            {
                item.TotalSallary =
                item.BasicSalary +
                item.Allowance +
                item.Transportation +
                _overTimePoliciesServices.Calculate(info.CalculatorName, item.BasicSalary + item.Allowance);
            }
            return personnelInfo;
        }

        public override async Task<PersonnelInfo> GetOneAsync(int id, CancellationToken cancellationToken)
        {
            var query = $"SELECT TOP(1) * FROM  PersonelInfo WHERE ID = {id}";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                var result = await conn.QueryAsync<PersonnelInfo>(query);

                return result.FirstOrDefault();
            }
        }
        public async Task<List<PersonnelInfo>> GetAllAsyncWithDapper(int pageSize, int pageNumber, bool ascSorted, string orderField, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken = default)
        {

            var orderColumn = orderField == null ? "ID" : orderField;
            var orderType = ascSorted ? "" : "DESC";

            var query = new StringBuilder($"SELECT * FROM  PersonelInfo ");

            bool hasFilterBefor = false;

            if (fromDate != null || toDate != null)
            {
                query.Append(" WHERE ");

                if (fromDate != null)
                {
                    query.Append(" SalaryDate >= @fromDate");
                    hasFilterBefor = true;
                }

                if (hasFilterBefor)
                    query.Append(" AND ");

                if (toDate != null)
                    query.Append(" SalaryDate <= @toDate");
            }


            query.Append($" ORDER By {orderColumn} ");

            query.Append($" {orderType} ");

            query.Append(" OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");

            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    var result = await conn.QueryAsync<PersonnelInfo>(query.ToString(), new
                    {
                        fromDate = fromDate,
                        toDate = toDate,
                        skip = (pageNumber - 1) * pageSize,
                        take = pageSize
                    });

                    return result.ToList();
                }
                catch (Exception ex)
                {

                    return null;
                }

            }

        }
    }
}
