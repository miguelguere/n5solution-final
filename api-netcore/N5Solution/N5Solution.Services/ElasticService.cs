﻿using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Solution.Services
{
    public class ElasticService
    {
        private string _indexName { get; set; }
        private readonly IElasticClient _client;

        public ElasticService(IElasticClient client)
        {
            _client = client;
            //_indexName = indexName;
        }

        public ElasticService Index(string indexName)
        {
            _indexName = indexName;
            return this;
        }

        public async Task CreateIndexIfNotExists(string indexName)
        {
            if (!_client.Indices.Exists(indexName).Exists)
            {
                await _client.Indices.CreateAsync(indexName, c => c.Map<dynamic>(m => m.AutoMap()));
            }
            Index(indexName);
        }

        public async Task<bool> AddOrUpdateBulk<T>(IEnumerable<T> documents) where T : class
        {
            var indexResponse = await _client.BulkAsync(b => b
                   .Index(_indexName)
                   .UpdateMany(documents, (ud, d) => ud.Doc(d).DocAsUpsert(true))
               );
            return indexResponse.IsValid;
        }
        public async Task<bool> AddOrUpdate<T>(T document) where T : class
        {
            try
            {
                var indexResponse = await _client.IndexAsync(document, idx => idx.Index(_indexName).OpType(OpType.Index));                

                if (!indexResponse.IsValid)
                {
                    throw new Exception(indexResponse.DebugInformation);
                }

                return indexResponse.IsValid;

            }
            catch (Exception ex)
            {
                throw;
            }
            
            
        }

        public async Task<T> Get<T>(string key) where T : class
        {
            var response = await _client.GetAsync<T>(key, g => g.Index(_indexName));
            return response.Source;
        }
        public async Task<List<T>?> GetAll<T>() where T : class
        {
            var searchResponse = await _client.SearchAsync<T>(s => s.Index(_indexName).Query(q => q.MatchAll()));
            return searchResponse.IsValid ? searchResponse.Documents.ToList() : default;
        }
        public async Task<List<T>?> Query<T>(QueryContainer predicate) where T : class
        {
            var searchResponse = await _client.SearchAsync<T>(s => s.Index(_indexName).Query(q => predicate));
            return searchResponse.IsValid ? searchResponse.Documents.ToList() : default;
        }
        public async Task<bool> Remove<T>(string key) where T : class
        {
            var response = await _client.DeleteAsync<T>(key, d => d.Index(_indexName));
            return response.IsValid;
        }
        public async Task<long> RemoveAll<T>() where T : class
        {
            var response = await _client.DeleteByQueryAsync<T>(d => d.Index(_indexName).Query(q => q.MatchAll()));
            return response.Deleted;
        }

    }
}
