using System;

namespace TodoistRest
{
    public class TodoistRestClient
    {
        public TodoistRestClient(string apiToken) 
            : this(new TodoistHttpHandler(apiToken)) {}

        public TodoistRestClient(ITodoistHttpHandler httpHandler)
        {
            ProjectsService = new ProjectsService(httpHandler);
        }

        public IProjectsService ProjectsService { get; private set; }
    }
}
