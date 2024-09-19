using Sample.API.Infrastructure.External.API.AI.open.ai;
using Sample.API.Persistence.Repositories;
using Sample.API.Services.Interfaces;
using System;

namespace Sample.API.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IGetAISQLResponse _getAISQL;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawSqlRepository<string> _rawSql;

        public OpenAIService(IUnitOfWork unitOfWork, IGetAISQLResponse getAISQL, IRawSqlRepository<string> rawSql)
        {
            _unitOfWork = unitOfWork;
            _getAISQL = getAISQL;
            _rawSql = rawSql;
        }

        public async Task<dynamic> GetSQLAsync(string chat)
        {
            return await _rawSql.ExecuteRawSqlAsync(await _getAISQL.GetSQLAsync(chat));

        }
    }
}
