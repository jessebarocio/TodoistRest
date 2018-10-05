using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoistRest
{
    public interface IProjectsService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
    }

    public class ProjectsService : IProjectsService
    {
        private readonly ITodoistHttpHandler _httpHandler;

        public ProjectsService(ITodoistHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _httpHandler.GetAsync<IEnumerable<Project>>("projects");
        }
    }
}