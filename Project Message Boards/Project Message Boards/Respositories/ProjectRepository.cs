using Project_Message_Boards.Models;

namespace Project_Message_Boards.Respositories
{
    public class ProjectRepository
    {
        private readonly Dictionary<string, Project> _projects = new();

        public Project GetOrCreate(string projectName)
        {
            if (!_projects.ContainsKey(projectName))
                _projects[projectName] = new Project { Name = projectName };

            return _projects[projectName];
        }
    }
}
