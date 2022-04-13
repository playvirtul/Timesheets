//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Timesheets.DataAccess.Postgre.Repositories
//{
//    public class ProjectsRepository
//    {
//        private readonly TimesheetsDbContext _context;

//        public ProjectsRepository(TimesheetsDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<Domain.Project[]> Get()
//        {
//            var projectEntities = await _context.Projects
//                .Include(p => p.WorkTimes)
//                .AsNoTracking()
//                .ToArrayAsync();

//            var projects = projectEntities
//                .Where(x => x != null)
//                .Select(x =>
//            {
//                var workTimes = x.WorkTimes
//                    .Select(y =>
//                    {
//                        var (workTime, errors) = Domain.WorkTime.Create(y.ProjectId, y.WorkingHours, y.Date);

//                        if (errors.Any() && workTime == null)
//                        {
//                            return (null, Errors: errors);
//                        }
//                        else
//                        {
//                            return (workTime, Errors: Array.Empty<string>());
//                        }
//                    })
//                    .Aggregate((new Domain.WorkTime[0], new string[0]), (a, b) =>
//                    {
//                        if (b.Errors.Any())
//                        {
//                            return b;
//                        }
//                        else
//                        {

//                        }
//                    });
//                    .ToArray();

//                var workTimesErrors = workTimes
//                    .Where(y => y.Errors.Any())
//                    .SelectMany(y => y.Errors).ToArray();

//                if (workTimesErrors.Any())
//                {
//                    return (null, workTimesErrors);
//                }
//                else
//                {
//                    var d = workTimes
//                .Select(y => y.workTime)
//                .Where(x => x != null)
//                .ToArray();

//                    var (projects, errors) = Domain.Project
//                        .Create(x.Title, x.Id, d);

//                    return (projects, errors);
//                }
//            });
//        }

//        public async Task<int> Create(Domain.Project project)
//        {

//        }
//    }
//}