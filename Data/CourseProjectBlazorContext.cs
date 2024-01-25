using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseProjectBlazor.Models;
using NuGet.Protocol;
using System.Globalization;

public class CourseProjectBlazorContext : DbContext
{
    public CourseProjectBlazorContext(DbContextOptions<CourseProjectBlazorContext> options)
        : base(options)
    {
    }

    public DbSet<CourseProjectBlazor.Models.Employer> Employer { get; set; } = default!;

    public DbSet<CourseProjectBlazor.Models.Applicant> Applicant { get; set; } = default!;

    private static readonly Random rd = new Random();
    public void GetData()
    {
        if (!Applicant.Any())
        {
            var data = ApplicantInfo();
            Applicant.AddRange(data);
            SaveChanges();
        }
    }
    public int Random()
    {
        return rd.Next(0,20);
    }
    public string GetPhoto()
    {
        int random = Random();
        string sourceFolderPath = "..\\CourseProjectBlazor\\wwwroot\\ga";
        string targetFolderPath = "..\\CourseProjectBlazor\\wwwroot\\img\\applicants";
        string[] files = Directory.GetFiles(sourceFolderPath);
        string sourceFile = files[random];
        string targetFile = Path.Combine(targetFolderPath, Path.GetFileName(sourceFile));

        File.Copy(sourceFile, targetFile, true);

        return Path.Combine("img", "applicants", Path.GetFileName(sourceFile));
    }

    public List<Applicant> ApplicantInfo()
    {
        List<string> lines = File.ReadAllLines("..\\CourseProjectBlazor\\wwwroot\\Base\\applicants.csv").ToList();
        var Applicant = new List<Applicant>();
        
        foreach (var line in lines)
        {
            var photo = GetPhoto();
            string[] spl = line.Split(';');
            var applicant = new Applicant
            {
                Фамилия = spl[0],
                Имя = spl[1],
                Отчество = spl[2],
                Образование = spl[3],
                Специальность = spl[4],
                Дата_Рождения = Convert.ToDateTime(spl[5]),
                Телефон = spl[6],
                Email = spl[7],
                Фото = photo
            };
            Applicant.Add(applicant);
        }
        return Applicant;
    }
}
