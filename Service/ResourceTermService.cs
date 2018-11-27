using Domain;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Domain.Entity;

namespace Service
{
    public class ResourceTermService : Pattern.IService<resource>
    {
        public void Add(resource entity)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Delete(resource entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<resource, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public resource Get(Expression<Func<resource, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public resource GetById(string id)
        {
            throw new NotImplementedException();
        }

        public resource GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<resource> GetMany(resource searchValues)
        {
            IEnumerable<resource> result = null;
            var requestObject = new { competence = new { idCompetence = searchValues.levels.First().idCompetence }, availables = searchValues.availability.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries) };
            result = RestApi.ApiFactory.PostRequest<List<resource>>(new Uri(Properties.Settings.Default.ListResourcesRequestUrl), requestObject);
            return result;
        }

        public IEnumerable<resource> GetMany(Expression<Func<resource, bool>> condition = null, Expression<Func<resource, bool>> orderBy = null)
        {
            var searchValue = new resource();
            searchValue.levels = new List<level>();
            searchValue.levels.Add(new level() { idCompetence = 1 });

            searchValue.availability = string.Join(";", "", "", "");
            GetMany(searchValue);
            throw new NotImplementedException();

        }

        public void Update(resource entity)
        {
            GetMany(r => r.levels.Any(l => l.idCompetence == 1) && r.availability == "", null);
            throw new NotImplementedException();
        }



        public static List<competence> FindCompetence()
        {
            Model1 context = new Model1();
            return context.competences.ToList();

        }

        public static List<project> FindProjects()
        {
            Model1 context = new Model1();
            return context.projects.ToList();

        }

        public IEnumerable<resource> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<resource> GetMany(Expression<Func<resource, bool>> condition = null, Expression<Func<resource, bool>> orderBy = null, string includeEntities = null)
        {
            throw new NotImplementedException();
        }
    }
}
