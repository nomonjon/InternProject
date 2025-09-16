using Education.Api.Data;
using Education.Api.Dtos;
using Education.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class FiltreRepo(EducationDbContext _dbContext) : IFilterRepo
{
    private readonly EducationDbContext dbContext = _dbContext;

    public async Task<IEnumerable<Dtos.Student>> FilterStudentsAsync(
        StudentFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Students
            .Include(s => s.City)
            .Include(s => s.Department)
            .Include(s => s.StudentSubjects)
            .AsQueryable();

        if (filter.CityId.HasValue)
            query = query.Where(s => s.CityId == filter.CityId.Value);

        if (filter.DepartmentId.HasValue)
            query = query.Where(s => s.DepartmentId == filter.DepartmentId.Value);

        if (filter.Gender.HasValue)
            query = query.Where(s => (int)s.Gender == filter.Gender.Value);

        if (filter.Grade.HasValue)
            query = query.Where(s => s.CurrentGradeLevel == filter.Grade.Value);

        if (filter.MinAge.HasValue)
        {
            var maxBirthDate = DateTime.UtcNow.AddYears(-filter.MinAge.Value);
            query = query.Where(s => s.BirthDate <= maxBirthDate);
        }

        if (filter.MaxAge.HasValue)
        {
            var minBirthDate = DateTime.UtcNow.AddYears(-filter.MaxAge.Value);
            query = query.Where(s => s.BirthDate >= minBirthDate);
        }

        if (filter.SubjectIds != null && filter.SubjectIds.Any())
        {
            query = query.Where(s =>
                s.StudentSubjects.Any(ss => filter.SubjectIds.Contains(ss.SubjectId)));
        }

        var students = await query.ToListAsync(cancellationToken);

        return students.Select(s => s.ToDto());
    }


    public async Task<IEnumerable<Dtos.Teacher>> FilterTeachersAsync(
        TeacherFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Teachers
            .Include(t => t.TeacherSubjects)
            .ThenInclude(ts => ts.Subject)
            .AsQueryable();

        if (filter.Gender.HasValue)
            query = query.Where(t => (int)t.Gender == filter.Gender.Value);

        if (filter.MinAge.HasValue)
        {
            var maxBirthDate = DateTime.UtcNow.AddYears(-filter.MinAge.Value);
            query = query.Where(t => t.BirthDate <= maxBirthDate);
        }

        if (filter.MaxAge.HasValue)
        {
            var minBirthDate = DateTime.UtcNow.AddYears(-filter.MaxAge.Value);
            query = query.Where(t => t.BirthDate >= minBirthDate);
        }

        if (filter.SubjectIds != null && filter.SubjectIds.Any())
        {
            query = query.Where(t =>
                t.TeacherSubjects.Any(ts => filter.SubjectIds.Contains(ts.SubjectId)));
        }

        var teachers = await query.ToListAsync(cancellationToken);

        return teachers.Select(t => t.ToDto());
    }


    public async Task<IEnumerable<Dtos.Student>> GetTop10StudentsBySubjectAsync(
        Guid subjectId,
        CancellationToken cancellationToken = default)
    {
        var students = await dbContext.StudentSubjects
            .Include(ss => ss.Student)
            .Where(ss => ss.SubjectId == subjectId)
            .OrderByDescending(ss => ss.Mark)
            .ThenByDescending(ss => ss.Student!.CurrentGradeLevel)
            .ThenBy(ss => ss.Student!.BirthDate)
            .Select(ss => ss.Student)
            .Distinct()
            .Take(10)
            .ToListAsync(cancellationToken);

        return students.Select(s => s!.ToDto());
    }


    public async Task<IEnumerable<Dtos.Teacher>> GetTop10TeachersByTopStudentsAsync(
        Guid subjectId,
        bool best,
        CancellationToken cancellationToken = default)
    {
        var studentsQuery = dbContext.StudentSubjects
            .Include(ss => ss.Student)
            .Where(ss => ss.SubjectId == subjectId);

        studentsQuery = best
            ? studentsQuery.OrderByDescending(ss => ss.Mark)
            : studentsQuery.OrderBy(ss => ss.Mark);

        var top5StudentIds = await studentsQuery
            .Select(ss => ss.StudentId)
            .Distinct()
            .Take(5)
            .ToListAsync(cancellationToken);

        var teachers = await dbContext.TeacherSubjects
            .Include(ts => ts.Teacher)
            .Where(ts => ts.SubjectId == subjectId)
            .Select(ts => ts.Teacher)
            .Distinct()
            .Take(10)
            .ToListAsync(cancellationToken);

        return teachers.Select(t => t!.ToDto());
    }


}