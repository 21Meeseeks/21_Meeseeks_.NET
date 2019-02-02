using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.ProjectStatService;

namespace Service
{
    public interface IProjectStatService : IService<project>
    {
        IDictionary<int, List<Sector>> ProjectsNumberBySectorGroupByYear();
        IDictionary<string,List<project>> ProjectsWithAdress();

        int GetNewProjects();
        int CountProjectsDone();
        int CountProjectsDone(string year);

        int countResource();
    }
}
